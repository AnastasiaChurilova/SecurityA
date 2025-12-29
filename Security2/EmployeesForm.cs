using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class EmployeesForm : Form
    {
        private string connectionString;
        private DataTable employeesDataTable;

        public EmployeesForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                // Упрощенный запрос БЕЗ алиасов или с английскими алиасами
                string sql = @"
            SELECT 
                ""ID_сотрудника"",
                ""ФИО"",
                ""Должность"",
                ""Дата_приёма"",
                ""Оклад"",
                ""Образование"",
                ""Лицензия_на_оружие"",
                ""Номер_лицензии"",
                ""Дата_выдачи_лицензии"",
                ""Уровень_доступа""
            FROM ""Сотрудники""
            ORDER BY ""ФИО""";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    var adapter = new FbDataAdapter(sql, connection);
                    employeesDataTable = new DataTable();
                    adapter.Fill(employeesDataTable);

                    dataGridView1.DataSource = employeesDataTable;

                    // Форматирование с оригинальными именами полей
                    FormatDataGridViewColumns();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FormatDataGridViewColumns()
        {
            // Используем оригинальные имена полей из БД
            if (dataGridView1.Columns.Contains("ID_сотрудника"))
            {
                dataGridView1.Columns["ID_сотрудника"].HeaderText = "ID";
                dataGridView1.Columns["ID_сотрудника"].Width = 50;
            }

            if (dataGridView1.Columns.Contains("ФИО"))
            {
                dataGridView1.Columns["ФИО"].HeaderText = "ФИО";
                dataGridView1.Columns["ФИО"].Width = 200;
            }

            if (dataGridView1.Columns.Contains("Должность"))
            {
                dataGridView1.Columns["Должность"].HeaderText = "Должность";
                dataGridView1.Columns["Должность"].Width = 150;
            }

            if (dataGridView1.Columns.Contains("Дата_приёма"))
            {
                dataGridView1.Columns["Дата_приёма"].HeaderText = "Дата приема";
                dataGridView1.Columns["Дата_приёма"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridView1.Columns["Дата_приёма"].Width = 100;
            }

            if (dataGridView1.Columns.Contains("Оклад"))
            {
                dataGridView1.Columns["Оклад"].HeaderText = "Оклад";
                dataGridView1.Columns["Оклад"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Оклад"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["Оклад"].Width = 100;
            }

            if (dataGridView1.Columns.Contains("Образование"))
            {
                dataGridView1.Columns["Образование"].HeaderText = "Образование";
                dataGridView1.Columns["Образование"].Width = 150;
            }

            if (dataGridView1.Columns.Contains("Лицензия_на_оружие"))
            {
                dataGridView1.Columns["Лицензия_на_оружие"].HeaderText = "Лицензия";
                dataGridView1.Columns["Лицензия_на_оружие"].Width = 80;
            }

            if (dataGridView1.Columns.Contains("Уровень_доступа"))
            {
                dataGridView1.Columns["Уровень_доступа"].HeaderText = "Уровень доступа";
                dataGridView1.Columns["Уровень_доступа"].Width = 100;
            }

            // Скрываем ненужные колонки
            if (dataGridView1.Columns.Contains("Номер_лицензии"))
                dataGridView1.Columns["Номер_лицензии"].Visible = false;

            if (dataGridView1.Columns.Contains("Дата_выдачи_лицензии"))
                dataGridView1.Columns["Дата_выдачи_лицензии"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new EmployeeEditForm(connectionString, 0);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Используем оригинальное имя поля
            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_сотрудника"].Value);
            var editForm = new EmployeeEditForm(connectionString, id);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            // Используем оригинальные имена полей
            var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_сотрудника"].Value);
            var name = dataGridView1.CurrentRow.Cells["ФИО"].Value.ToString();

            if (MessageBox.Show($"Удалить сотрудника {name}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Проверяем, есть ли связанные записи
                    string checkContractsSql = "SELECT COUNT(*) FROM \"Договоры\" WHERE \"ID_сотрудника\" = @id";
                    string checkIncidentsSql = "SELECT COUNT(*) FROM \"Инциденты\" WHERE \"ID_сотрудника\" = @id";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();

                        // Проверка договоров
                        using (var cmd = new FbCommand(checkContractsSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int contractCount = Convert.ToInt32(cmd.ExecuteScalar());

                            if (contractCount > 0)
                            {
                                MessageBox.Show($"Невозможно удалить сотрудника. Существуют договоры ({contractCount} шт.)",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Проверка инцидентов
                        using (var cmd = new FbCommand(checkIncidentsSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int incidentCount = Convert.ToInt32(cmd.ExecuteScalar());

                            if (incidentCount > 0)
                            {
                                MessageBox.Show($"Невозможно удалить сотрудника. Существуют инциденты ({incidentCount} шт.)",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Удаляем сотрудника
                        string deleteSql = "DELETE FROM \"Сотрудники\" WHERE \"ID_сотрудника\" = @id";
                        using (var cmd = new FbCommand(deleteSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadEmployees();
                    MessageBox.Show("Сотрудник удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления сотрудника:\n{ex.Message}",
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
            LoadEmployees();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (employeesDataTable != null)
            {
                string searchText = txtSearch.Text.ToLower();

                try
                {
                    string filter = "";

                    // Используем оригинальные имена полей для фильтрации
                    if (employeesDataTable.Columns.Contains("ФИО"))
                        filter += $"ФИО LIKE '%{searchText}%' OR ";

                    if (employeesDataTable.Columns.Contains("Должность"))
                        filter += $"Должность LIKE '%{searchText}%' OR ";

                    if (employeesDataTable.Columns.Contains("Образование"))
                        filter += $"Образование LIKE '%{searchText}%' OR ";

                    // Убираем последнее " OR "
                    if (filter.Length > 0)
                        filter = filter.Remove(filter.Length - 4);

                    employeesDataTable.DefaultView.RowFilter = filter;
                }
                catch
                {
                    // Игнорируем ошибки фильтрации
                }
            }
        }
    }
}