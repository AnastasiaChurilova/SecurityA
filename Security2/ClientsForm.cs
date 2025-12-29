using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class ClientsForm : Form
    {
        private string connectionString;
        private DataTable clientsDataTable;

        public ClientsForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            string sql = "";

            try
            {
                // Запрос с английскими алиасами для избежания проблем
                sql = @"
                    SELECT 
                        ""ID_заказчика"" as ClientID,
                        ""Тип_заказчика"" as ClientType,
                        ""Наименование_или_ФИО"" as ClientName,
                        ""Адрес"" as Address,
                        ""Телефон"" as Phone,
                        ""Email"" as Email,
                        ""Номер_счёта"" as AccountNumber,
                        ""ФИО_представителя"" as Representative
                    FROM ""Заказчики""
                    ORDER BY ""Наименование_или_ФИО""";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    using (var cmd = new FbCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        clientsDataTable = new DataTable();
                        clientsDataTable.Load(reader);

                        dataGridView1.DataSource = clientsDataTable;

                        // Переименовываем заголовки на русском
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            switch (column.Name)
                            {
                                case "ClientID":
                                    column.HeaderText = "ID";
                                    column.Width = 50;
                                    break;
                                case "ClientType":
                                    column.HeaderText = "Тип";
                                    column.Width = 120;
                                    break;
                                case "ClientName":
                                    column.HeaderText = "Наименование";
                                    column.Width = 200;
                                    break;
                                case "Address":
                                    column.HeaderText = "Адрес";
                                    column.Width = 200;
                                    break;
                                case "Phone":
                                    column.HeaderText = "Телефон";
                                    column.Width = 120;
                                    break;
                                case "Email":
                                    column.HeaderText = "Email";
                                    column.Width = 150;
                                    break;
                                case "AccountNumber":
                                    column.HeaderText = "Номер счета";
                                    column.Width = 120;
                                    break;
                                case "Representative":
                                    column.HeaderText = "Представитель";
                                    column.Width = 150;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new ClientEditForm(connectionString, 0);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadClients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите клиента для редактирования",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Получаем ID из правильной колонки
            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ClientID"].Value);
            var editForm = new ClientEditForm(connectionString, id);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadClients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            // Получаем данные из правильных колонок
            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ClientID"].Value);
            var name = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();

            if (MessageBox.Show($"Удалить клиента {name}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Проверяем, есть ли договоры у клиента
                    string checkSql = "SELECT COUNT(*) FROM \"Договоры\" WHERE \"ID_заказчика\" = @id";
                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(checkSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int contractCount = Convert.ToInt32(cmd.ExecuteScalar());

                            if (contractCount > 0)
                            {
                                MessageBox.Show($"Невозможно удалить клиента. Существуют договоры ({contractCount} шт.)",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Удаляем клиента
                        string deleteSql = "DELETE FROM \"Заказчики\" WHERE \"ID_заказчика\" = @id";
                        using (var cmd = new FbCommand(deleteSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadClients();
                    MessageBox.Show("Клиент удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления клиента:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadClients();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (clientsDataTable != null)
            {
                string searchText = txtSearch.Text.ToLower();

                // Используем английские имена колонок для фильтрации
                clientsDataTable.DefaultView.RowFilter =
                    $"ClientName LIKE '%{searchText}%' OR " +
                    $"Address LIKE '%{searchText}%' OR " +
                    $"Phone LIKE '%{searchText}%' OR " +
                    $"Email LIKE '%{searchText}%'";
            }
        }
    }
}