using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;

namespace Security2
{
    public partial class ContractsForm : Form
    {
        private string connectionString;
        private DataTable contractsDataTable;

        public ContractsForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadContracts();
            InitializeFilters();
        }

        private void LoadContracts()
        {
            string sql = "";

            try
            {
                // Рабочий запрос из предыдущей версии
                sql = @"
                    SELECT 
                        d.""ID_договора"" as ContractID,
                        d.""Дата_заказа"" as OrderDate,
                        d.""Статус"" as Status,
                        d.""Сумма"" as Amount,
                        COALESCE(z.""Наименование_или_ФИО"", 'Не указан') as ClientName
                    FROM ""Договоры"" d
                    LEFT JOIN ""Заказчики"" z ON d.""ID_заказчика"" = z.""ID_заказчика""
                    ORDER BY d.""Дата_заказа"" DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    using (var cmd = new FbCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        contractsDataTable = new DataTable();
                        contractsDataTable.Load(reader);

                        dataGridView1.DataSource = contractsDataTable;

                        // Форматирование колонок
                        FormatDataGridViewColumns();
                    }

                    UpdateStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки договоров:\n{ex.Message}\n\nSQL: {sql}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormatDataGridViewColumns()
        {
            // Проверяем имена колонок после загрузки данных
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                switch (column.Name)
                {
                    case "ContractID":  // <-- Исправлено с ID_договора на ContractID
                        column.HeaderText = "№";
                        column.Width = 60;
                        break;
                    case "OrderDate":   // <-- Исправлено с Дата_заказа на OrderDate
                        column.HeaderText = "Дата заказа";
                        column.DefaultCellStyle.Format = "dd.MM.yyyy";
                        column.Width = 100;
                        break;
                    case "Status":      // <-- Исправлено с Статус на Status
                        column.HeaderText = "Статус";
                        column.Width = 100;
                        break;
                    case "Amount":      // <-- Исправлено с Сумма на Amount
                        column.HeaderText = "Сумма";
                        column.DefaultCellStyle.Format = "C2";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        column.Width = 100;
                        break;
                    case "ClientName":  // <-- Исправлено с Клиент на ClientName
                        column.HeaderText = "Клиент";
                        column.Width = 200;
                        break;
                }
            }
        }

        private void InitializeFilters()
        {
            // Заполняем комбобокс статусов
            cmbStatusFilter.Items.Add("Все");
            cmbStatusFilter.Items.Add("Активен");
            cmbStatusFilter.Items.Add("Завершён");
            cmbStatusFilter.SelectedIndex = 0;

            // Устанавливаем диапазон дат
            dtpDateFrom.Value = DateTime.Today.AddMonths(-1);
            dtpDateTo.Value = DateTime.Today;
        }

        private void UpdateStatistics()
        {
            if (contractsDataTable == null) return;

            // Работаем с текущим представлением (уже отфильтрованным)
            var currentView = contractsDataTable.DefaultView;

            int totalCount = currentView.Count;
            decimal totalAmount = 0;
            int activeCount = 0;

            foreach (DataRowView rowView in currentView)
            {
                var row = rowView.Row;

                // Используем английские имена колонок
                if (row["Amount"] != DBNull.Value)
                {
                    totalAmount += Convert.ToDecimal(row["Amount"]);
                }

                if (row["Status"]?.ToString() == "Активен")
                {
                    activeCount++;
                }
            }

            lblTotalContracts.Text = $"Всего: {totalCount}";
            lblActiveContracts.Text = $"Активных: {activeCount}";
            lblTotalAmount.Text = $"Общая сумма: {totalAmount:C2}";
        }

        private void ApplyFilters()
        {
            if (contractsDataTable == null) return;

            var filteredView = contractsDataTable.DefaultView;
            string filter = "";

            // Фильтр по дате - используем OrderDate
            if (chkFilterByDate.Checked)
            {
                filter += $"(OrderDate >= '{dtpDateFrom.Value:yyyy-MM-dd}' AND OrderDate <= '{dtpDateTo.Value:yyyy-MM-dd}')";
            }

            // Фильтр по статусу - используем Status
            if (cmbStatusFilter.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += $"(Status = '{cmbStatusFilter.SelectedItem}')";
            }

            // Применяем фильтр к DefaultView
            filteredView.RowFilter = filter;

            // Обновляем статистику по отфильтрованным данным
            UpdateStatistics();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            cmbStatusFilter.SelectedIndex = 0;
            chkFilterByDate.Checked = false;

            if (contractsDataTable != null)
            {
                // Сбрасываем фильтр
                contractsDataTable.DefaultView.RowFilter = "";
                UpdateStatistics();
            }
        }

        private void btnAddContract_Click(object sender, EventArgs e)
        {
            var editForm = new ContractEditForm(connectionString, 0);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadContracts();
            }
        }

        private void btnEditContract_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите договор для редактирования",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Получаем данные из оригинальной таблицы через DataBoundItem
            var rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
            if (rowView == null) return;

            var row = rowView.Row;

            // ИСПРАВЛЕНИЕ: Используем ContractID
            var id = Convert.ToInt32(row["ContractID"]);

            var editForm = new ContractEditForm(connectionString, id);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadContracts();
            }
        }

        private void btnDeleteContract_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            // Получаем данные из оригинальной таблицы
            var rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
            if (rowView == null) return;

            var row = rowView.Row;

            // ИСПРАВЛЕНИЕ: Используем ContractID
            var id = Convert.ToInt32(row["ContractID"]);
            var date = Convert.ToDateTime(row["OrderDate"]).ToString("dd.MM.yyyy");
            var amount = Convert.ToDecimal(row["Amount"]).ToString("C2");

            if (MessageBox.Show($"Удалить договор №{id} от {date} на сумму {amount}?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // ИСПРАВЛЕНИЕ: Используем оригинальные имена полей в SQL
                    string deletePaymentsSql = "DELETE FROM \"Платежи\" WHERE \"ID_договора\" = @id";
                    string deleteContractSql = "DELETE FROM \"Договоры\" WHERE \"ID_договора\" = @id";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();

                        // Удаляем платежи
                        using (var cmd = new FbCommand(deletePaymentsSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        // Удаляем договор
                        using (var cmd = new FbCommand(deleteContractSql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadContracts();
                    MessageBox.Show("Договор удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления договора:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewPayments_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем выбор строки
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Выберите договор из списка",
                                  "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем значение через DataBoundItem
                var rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (rowView == null)
                {
                    MessageBox.Show("Не удалось получить данные договора",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ИСПРАВЛЕНИЕ: Используем ContractID, а не ID_договора
                // В запросе SQL мы использовали: d."ID_договора" as ContractID
                var contractId = Convert.ToInt32(rowView.Row["ContractID"]);

                // Создаем и показываем форму
                using (PaymentsForm form = new PaymentsForm(connectionString, contractId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия платежей: {ex.Message}\n\n" +
                               $"Детали: {ex.InnerException?.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadContracts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtClientFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxFilter_Enter(object sender, EventArgs e)
        {

        }
    }
}