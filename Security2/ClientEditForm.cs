using System;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class ClientEditForm : Form
    {
        private string connectionString;
        private int clientId;
        private bool isEditMode;

        public ClientEditForm(string connectionString, int clientId = 0)
        {
            this.connectionString = connectionString;
            this.clientId = clientId;
            this.isEditMode = clientId > 0;

            InitializeComponent();

            if (isEditMode)
            {
                LoadClientData();
                this.Text = "Редактирование клиента";
            }
            else
            {
                this.Text = "Добавление нового клиента";
                cmbClientType.SelectedIndex = 0;
            }
        }

        private void LoadClientData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ""Тип_заказчика"",
                        ""Наименование_или_ФИО"",
                        ""Адрес"",
                        ""Номер_счёта"",
                        ""ФИО_представителя"",
                        ""Телефон"",
                        ""Email""
                    FROM ""Заказчики""
                    WHERE ""ID_заказчика"" = @id";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", clientId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmbClientType.Text = reader.GetString(0);
                                txtName.Text = reader.GetString(1);
                                txtAddress.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                txtAccountNumber.Text = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                txtRepresentative.Text = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                txtPhone.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);
                                txtEmail.Text = reader.IsDBNull(6) ? "" : reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных клиента:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(cmbClientType.Text))
            {
                MessageBox.Show("Выберите тип клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClientType.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Введите наименование/ФИО клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                string clientType = cmbClientType.Text;
                string name = txtName.Text;
                string address = txtAddress.Text;
                string accountNumber = txtAccountNumber.Text;
                string representative = txtRepresentative.Text;
                string phone = txtPhone.Text;
                string email = txtEmail.Text;

                if (isEditMode)
                {
                    UpdateClient(clientType, name, address, accountNumber, representative, phone, email);
                }
                else
                {
                    InsertClient(clientType, name, address, accountNumber, representative, phone, email);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения клиента:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertClient(string clientType, string name, string address, string accountNumber,
                                 string representative, string phone, string email)
        {
            string sql = @"
                INSERT INTO ""Заказчики"" (
                    ""Тип_заказчика"",
                    ""Наименование_или_ФИО"",
                    ""Адрес"",
                    ""Номер_счёта"",
                    ""ФИО_представителя"",
                    ""Телефон"",
                    ""Email""
                ) VALUES (
                    @clientType, @name, @address, @accountNumber, 
                    @representative, @phone, @email
                )";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@clientType", clientType);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                    cmd.Parameters.AddWithValue("@accountNumber", string.IsNullOrEmpty(accountNumber) ? (object)DBNull.Value : accountNumber);
                    cmd.Parameters.AddWithValue("@representative", string.IsNullOrEmpty(representative) ? (object)DBNull.Value : representative);
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateClient(string clientType, string name, string address, string accountNumber,
                                 string representative, string phone, string email)
        {
            string sql = @"
                UPDATE ""Заказчики"" SET
                    ""Тип_заказчика"" = @clientType,
                    ""Наименование_или_ФИО"" = @name,
                    ""Адрес"" = @address,
                    ""Номер_счёта"" = @accountNumber,
                    ""ФИО_представителя"" = @representative,
                    ""Телефон"" = @phone,
                    ""Email"" = @email
                WHERE ""ID_заказчика"" = @id";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", clientId);
                    cmd.Parameters.AddWithValue("@clientType", clientType);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                    cmd.Parameters.AddWithValue("@accountNumber", string.IsNullOrEmpty(accountNumber) ? (object)DBNull.Value : accountNumber);
                    cmd.Parameters.AddWithValue("@representative", string.IsNullOrEmpty(representative) ? (object)DBNull.Value : representative);
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}