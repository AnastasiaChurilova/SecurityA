using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Security2
{
    public partial class IncidentsForm : Form
    {
        private string connectionString;
        private DataTable incidentsDataTable;

        public IncidentsForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadIncidents();
        }

        private void LoadIncidents()
        {
            string sql = "";

            try
            {
                // ПРОСТО БЕЗ JOIN - только основные поля
                sql = @"
            SELECT 
                i.""ID_инцидента"",
                i.""Дата_время"",
                i.""Тип_инцидента"",
                i.""Статус"",
                i.""ID_объекта"",
                i.""ID_сотрудника"",
                i.""Описание""
            FROM ""Инциденты"" i
            ORDER BY i.""Дата_время"" DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    var adapter = new FbDataAdapter(sql, connection);
                    incidentsDataTable = new DataTable();
                    adapter.Fill(incidentsDataTable);

                    dataGridView1.DataSource = incidentsDataTable;
                    SimpleFormatDataGridView();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SimpleFormatDataGridView()
        {
            // Простое форматирование
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Columns.Contains("ID_инцидента"))
            {
                dataGridView1.Columns["ID_инцидента"].HeaderText = "№";
                dataGridView1.Columns["ID_инцидента"].Width = 50;
            }

            if (dataGridView1.Columns.Contains("Дата_время"))
            {
                dataGridView1.Columns["Дата_время"].HeaderText = "Дата и время";
                dataGridView1.Columns["Дата_время"].Width = 120;
            }

            if (dataGridView1.Columns.Contains("Тип_инцидента"))
            {
                dataGridView1.Columns["Тип_инцидента"].HeaderText = "Тип";
                dataGridView1.Columns["Тип_инцидента"].Width = 150;
            }

            if (dataGridView1.Columns.Contains("Статус"))
            {
                dataGridView1.Columns["Статус"].HeaderText = "Статус";
                dataGridView1.Columns["Статус"].Width = 100;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new IncidentEditForm(connectionString, 0);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadIncidents();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите инцидент для редактирования",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_инцидента"].Value);
            var editForm = new IncidentEditForm(connectionString, id);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadIncidents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_инцидента"].Value);
            var incidentType = dataGridView1.CurrentRow.Cells["Тип_инцидента"].Value?.ToString() ?? "";
            var date = dataGridView1.CurrentRow.Cells["Дата_время"].Value != null
                ? Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Дата_время"].Value).ToString("dd.MM.yyyy HH:mm")
                : "";

            if (MessageBox.Show($"Удалить инцидент от {date}?\nТип: {incidentType}",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM \"Инциденты\" WHERE \"ID_инцидента\" = @id";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadIncidents();
                    MessageBox.Show("Инцидент удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления инцидента:\n{ex.Message}",
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
            LoadIncidents();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (incidentsDataTable != null)
            {
                string searchText = txtSearch.Text.ToLower();

                try
                {
                    string filter = "";

                    if (incidentsDataTable.Columns.Contains("Тип_инцидента"))
                        filter += $"Тип_инцидента LIKE '%{searchText}%' OR ";

                    if (incidentsDataTable.Columns.Contains("Статус"))
                        filter += $"Статус LIKE '%{searchText}%' OR ";

                    if (incidentsDataTable.Columns.Contains("Объект"))
                        filter += $"Объект LIKE '%{searchText}%' OR ";

                    if (incidentsDataTable.Columns.Contains("Сотрудник"))
                        filter += $"Сотрудник LIKE '%{searchText}%' OR ";

                    if (incidentsDataTable.Columns.Contains("Описание"))
                        filter += $"Описание LIKE '%{searchText}%' OR ";

                    // Убираем последнее " OR "
                    if (filter.Length > 0)
                        filter = filter.Remove(filter.Length - 4);

                    incidentsDataTable.DefaultView.RowFilter = filter;
                }
                catch
                {
                    // Игнорируем ошибки фильтрации
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Можно добавить обработку кликов по ячейкам
        }
    }
}