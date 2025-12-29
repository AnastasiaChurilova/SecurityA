using System;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class PaymentEditForm : Form
    {
        private string connectionString;
        private int paymentId;
        private int contractId;
        private bool isEditMode;

        public PaymentEditForm(string connectionString, int paymentId = 0, int contractId = 0)
        {
            this.connectionString = connectionString;
            this.paymentId = paymentId;
            this.contractId = contractId;
            this.isEditMode = paymentId > 0;

            InitializeComponent();

            if (isEditMode)
            {
                LoadPaymentData();
                this.Text = "Редактирование платежа №" + paymentId;
            }
            else
            {
                this.Text = "Добавление нового платежа";
                dtpPaymentDate.Value = DateTime.Now;
                cmbStatus.SelectedIndex = 0;
                cmbPaymentMethod.SelectedIndex = 0;
            }
        }

        private void LoadPaymentData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ""Дата_платежа"",
                        ""Сумма"",
                        ""Способ_оплаты"",
                        ""Статус""
                    FROM ""Платежи""
                    WHERE ""ID_платежа"" = @id";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", paymentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dtpPaymentDate.Value = reader.GetDateTime(0);
                                txtAmount.Text = reader.GetDecimal(1).ToString("F2");
                                cmbPaymentMethod.Text = reader.GetString(2);
                                cmbStatus.Text = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных платежа:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму платежа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbPaymentMethod.Text))
            {
                MessageBox.Show("Выберите способ оплаты", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPaymentMethod.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                MessageBox.Show("Выберите статус платежа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                DateTime paymentDate = dtpPaymentDate.Value;
                decimal amount = decimal.Parse(txtAmount.Text);
                string paymentMethod = cmbPaymentMethod.Text;
                string status = cmbStatus.Text;

                if (isEditMode)
                {
                    UpdatePayment(paymentDate, amount, paymentMethod, status);
                }
                else
                {
                    InsertPayment(paymentDate, amount, paymentMethod, status);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения платежа:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertPayment(DateTime paymentDate, decimal amount, string paymentMethod, string status)
        {
            string sql = @"
                INSERT INTO ""Платежи"" (
                    ""ID_договора"",
                    ""Дата_платежа"",
                    ""Сумма"",
                    ""Способ_оплаты"",
                    ""Статус""
                ) VALUES (
                    @contractId, @paymentDate, @amount, @paymentMethod, @status
                )";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@contractId", contractId);
                    cmd.Parameters.AddWithValue("@paymentDate", paymentDate);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdatePayment(DateTime paymentDate, decimal amount, string paymentMethod, string status)
        {
            string sql = @"
                UPDATE ""Платежи"" SET
                    ""Дата_платежа"" = @paymentDate,
                    ""Сумма"" = @amount,
                    ""Способ_оплаты"" = @paymentMethod,
                    ""Статус"" = @status
                WHERE ""ID_платежа"" = @id";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", paymentId);
                    cmd.Parameters.AddWithValue("@paymentDate", paymentDate);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PaymentEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}