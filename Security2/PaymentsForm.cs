using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class PaymentsForm : Form
    {
        private string connectionString;
        private int contractId;
        private DataTable paymentsDataTable;

        public PaymentsForm(string connectionString, int contractId)
        {
            this.connectionString = connectionString;
            this.contractId = contractId;

            // ИНИЦИАЛИЗИРУЕМ КОМПОНЕНТЫ ПЕРВЫМИ!
            InitializeComponent();

            // Теперь загружаем данные
            try
            {
                LoadContractInfo();
                LoadPayments();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadContractInfo()
        {
            try
            {
                string sql = @"
            SELECT 
                d.""ID_договора"",
                d.""Дата_заказа"",
                d.""Сумма"",
                z.""Наименование_или_ФИО"" as ClientName
            FROM ""Договоры"" d
            JOIN ""Заказчики"" z ON d.""ID_заказчика"" = z.""ID_заказчика""
            WHERE d.""ID_договора"" = @id";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", contractId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblContractInfo.Text = $"Договор №{reader.GetInt32(0)} от {reader.GetDateTime(1):dd.MM.yyyy}";
                                lblClientInfo.Text = $"Клиент: {reader.GetString(3)}";
                                lblContractAmount.Text = $"Сумма договора: {reader.GetDecimal(2):C2}";
                            }
                            else
                            {
                                MessageBox.Show($"Договор с ID {contractId} не найден",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // НЕ закрываем форму здесь, просто показываем сообщение
                                lblContractInfo.Text = "Договор не найден";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadPayments()
        {
            try
            {
                string sql = @"
            SELECT 
                ""ID_платежа"",
                ""Дата_платежа"",
                ""Сумма"",
                ""Способ_оплаты"",
                ""Статус""
            FROM ""Платежи""
            WHERE ""ID_договора"" = @contractId
            ORDER BY ""Дата_платежа"" DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    var adapter = new FbDataAdapter(sql, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@contractId", contractId);

                    paymentsDataTable = new DataTable();
                    adapter.Fill(paymentsDataTable);

                    dataGridView1.DataSource = paymentsDataTable;
                    FormatDataGridViewColumns();
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FormatDataGridViewColumns()
        {
            if (dataGridView1.Columns.Contains("ID_платежа"))
            {
                dataGridView1.Columns["ID_платежа"].HeaderText = "№";
                dataGridView1.Columns["ID_платежа"].Width = 50;
            }

            if (dataGridView1.Columns.Contains("Дата_платежа"))
            {
                dataGridView1.Columns["Дата_платежа"].HeaderText = "Дата платежа";
                dataGridView1.Columns["Дата_платежа"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridView1.Columns["Дата_платежа"].Width = 120;
            }

            if (dataGridView1.Columns.Contains("Сумма"))
            {
                dataGridView1.Columns["Сумма"].HeaderText = "Сумма";
                dataGridView1.Columns["Сумма"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Сумма"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["Сумма"].Width = 100;
            }

            if (dataGridView1.Columns.Contains("Способ_оплаты"))
            {
                dataGridView1.Columns["Способ_оплаты"].HeaderText = "Способ оплаты";
                dataGridView1.Columns["Способ_оплаты"].Width = 150;
            }

            if (dataGridView1.Columns.Contains("Статус"))
            {
                dataGridView1.Columns["Статус"].HeaderText = "Статус";
                dataGridView1.Columns["Статус"].Width = 100;
            }
        }

        private void CalculateTotals()
        {
            decimal totalPaid = 0;
            decimal totalPending = 0;

            foreach (DataRow row in paymentsDataTable.Rows)
            {
                if (row["Сумма"] != DBNull.Value)
                {
                    decimal amount = Convert.ToDecimal(row["Сумма"]);
                    string status = row["Статус"]?.ToString();

                    if (status == "Оплачено")
                        totalPaid += amount;
                    else
                        totalPending += amount;
                }
            }

            lblTotalPaid.Text = $"Оплачено: {totalPaid:C2}";
            lblTotalPending.Text = $"Ожидает: {totalPending:C2}";
            lblBalance.Text = $"Итого: {(totalPaid + totalPending):C2}";
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            var paymentForm = new PaymentEditForm(connectionString, 0, contractId);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                LoadPayments();
            }
        }

        private void btnEditPayment_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите платеж для редактирования",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var paymentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_платежа"].Value);
            var paymentForm = new PaymentEditForm(connectionString, paymentId, contractId);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                LoadPayments();
            }
        }

        private void btnDeletePayment_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var paymentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_платежа"].Value);
            var amount = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Сумма"].Value);
            var date = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Дата_платежа"].Value).ToString("dd.MM.yyyy");

            if (MessageBox.Show($"Удалить платеж №{paymentId} от {date} на сумму {amount:C2}?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM \"Платежи\" WHERE \"ID_платежа\" = @id";
                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", paymentId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadPayments();
                    MessageBox.Show("Платеж удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления платежа:\n{ex.Message}",
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
            LoadPayments();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}