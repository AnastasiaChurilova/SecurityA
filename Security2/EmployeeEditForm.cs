using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace Security2
{
    public partial class EmployeeEditForm : Form
    {
        private string connectionString;
        private int employeeId;
        private bool isEditMode;
        private byte[] photoData;

        public EmployeeEditForm(string connectionString, int employeeId = 0)
        {
            this.connectionString = connectionString;
            this.employeeId = employeeId;
            this.isEditMode = employeeId > 0;

            InitializeComponent();

            // Заполняем комбобоксы
            cmbEducation.Items.AddRange(new string[] { "Среднее", "Среднее специальное", "Высшее", "Неоконченное высшее" });
            cmbAccessLevel.Items.AddRange(new string[] { "1", "2", "3", "4", "5" });

            if (isEditMode)
            {
                LoadEmployeeData();
                this.Text = "Редактирование сотрудника";
            }
            else
            {
                this.Text = "Добавление нового сотрудника";
                dtpHireDate.Value = DateTime.Today;
                cmbAccessLevel.SelectedIndex = 0;
                chkLicense.Checked = false;
            }
        }

        private void LoadEmployeeData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ""ФИО"",
                        ""Должность"",
                        ""Дата_приёма"",
                        ""Оклад"",
                        ""Образование"",
                        ""Лицензия_на_оружие"",
                        ""Номер_лицензии"",
                        ""Дата_выдачи_лицензии"",
                        ""Уровень_доступа"",
                        ""ФОТОГРАФИЯ""
                    FROM ""Сотрудники""
                    WHERE ""ID_сотрудника"" = @id";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader.GetString(0);
                                txtPosition.Text = reader.GetString(1);
                                dtpHireDate.Value = reader.GetDateTime(2);
                                txtSalary.Text = reader.GetDecimal(3).ToString("F2");

                                if (!reader.IsDBNull(4))
                                    cmbEducation.Text = reader.GetString(4);

                                chkLicense.Checked = reader.GetBoolean(5);

                                if (!reader.IsDBNull(6))
                                    txtLicenseNumber.Text = reader.GetString(6);

                                if (!reader.IsDBNull(7))
                                    dtpLicenseDate.Value = reader.GetDateTime(7);
                                else
                                    chkHasLicenseDate.Checked = false;

                                cmbAccessLevel.Text = reader.GetInt32(8).ToString();

                                // Загружаем фото если есть
                                if (!reader.IsDBNull(9))
                                {
                                    using (var stream = reader.GetStream(9))
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        stream.CopyTo(memoryStream);
                                        photoData = memoryStream.ToArray();
                                        DisplayPhoto();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных сотрудника:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPhoto()
        {
            if (photoData != null && photoData.Length > 0)
            {
                using (var ms = new MemoryStream(photoData))
                {
                    try
                    {
                        pbPhoto.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                        pbPhoto.Image = null;
                        MessageBox.Show("Не удалось загрузить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                pbPhoto.Image = null;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО сотрудника", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPosition.Text))
            {
                MessageBox.Show("Введите должность", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPosition.Focus();
                return false;
            }

            if (!decimal.TryParse(txtSalary.Text, out decimal salary) || salary <= 0)
            {
                MessageBox.Show("Введите корректный оклад", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSalary.Focus();
                return false;
            }

            if (chkLicense.Checked && string.IsNullOrEmpty(txtLicenseNumber.Text))
            {
                MessageBox.Show("Введите номер лицензии", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseNumber.Focus();
                return false;
            }

            if (chkLicense.Checked && chkHasLicenseDate.Checked && dtpLicenseDate.Value > DateTime.Today)
            {
                MessageBox.Show("Дата выдачи лицензии не может быть в будущем", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpLicenseDate.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                string fullName = txtFullName.Text;
                string position = txtPosition.Text;
                DateTime hireDate = dtpHireDate.Value;
                decimal salary = decimal.Parse(txtSalary.Text);
                string education = cmbEducation.Text;
                bool hasLicense = chkLicense.Checked;
                string licenseNumber = hasLicense ? txtLicenseNumber.Text : null;
                DateTime? licenseDate = (hasLicense && chkHasLicenseDate.Checked) ? (DateTime?)dtpLicenseDate.Value : null;
                int accessLevel = int.Parse(cmbAccessLevel.Text);

                if (isEditMode)
                {
                    UpdateEmployee(fullName, position, hireDate, salary, education,
                                 hasLicense, licenseNumber, licenseDate, accessLevel);
                }
                else
                {
                    InsertEmployee(fullName, position, hireDate, salary, education,
                                 hasLicense, licenseNumber, licenseDate, accessLevel);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения сотрудника:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertEmployee(string fullName, string position, DateTime hireDate, decimal salary,
                                   string education, bool hasLicense, string licenseNumber,
                                   DateTime? licenseDate, int accessLevel)
        {
            string sql = @"
                INSERT INTO ""Сотрудники"" (
                    ""ФИО"",
                    ""Должность"",
                    ""Дата_приёма"",
                    ""Оклад"",
                    ""Образование"",
                    ""Лицензия_на_оружие"",
                    ""Номер_лицензии"",
                    ""Дата_выдачи_лицензии"",
                    ""Уровень_доступа"",
                    ""ФОТОГРАФИЯ""
                ) VALUES (
                    @fullName, @position, @hireDate, @salary, @education,
                    @hasLicense, @licenseNumber, @licenseDate, @accessLevel, @photo
                )";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@hireDate", hireDate);
                    cmd.Parameters.AddWithValue("@salary", salary);
                    cmd.Parameters.AddWithValue("@education", string.IsNullOrEmpty(education) ? (object)DBNull.Value : education);
                    cmd.Parameters.AddWithValue("@hasLicense", hasLicense);
                    cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? (object)DBNull.Value : licenseNumber);
                    cmd.Parameters.AddWithValue("@licenseDate", licenseDate.HasValue ? (object)licenseDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@accessLevel", accessLevel);

                    // Параметр для фото
                    if (photoData != null && photoData.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@photo", photoData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateEmployee(string fullName, string position, DateTime hireDate, decimal salary,
                                   string education, bool hasLicense, string licenseNumber,
                                   DateTime? licenseDate, int accessLevel)
        {
            string sql = @"
                UPDATE ""Сотрудники"" SET
                    ""ФИО"" = @fullName,
                    ""Должность"" = @position,
                    ""Дата_приёма"" = @hireDate,
                    ""Оклад"" = @salary,
                    ""Образование"" = @education,
                    ""Лицензия_на_оружие"" = @hasLicense,
                    ""Номер_лицензии"" = @licenseNumber,
                    ""Дата_выдачи_лицензии"" = @licenseDate,
                    ""Уровень_доступа"" = @accessLevel,
                    ""ФОТОГРАФИЯ"" = @photo
                WHERE ""ID_сотрудника"" = @id";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@hireDate", hireDate);
                    cmd.Parameters.AddWithValue("@salary", salary);
                    cmd.Parameters.AddWithValue("@education", string.IsNullOrEmpty(education) ? (object)DBNull.Value : education);
                    cmd.Parameters.AddWithValue("@hasLicense", hasLicense);
                    cmd.Parameters.AddWithValue("@licenseNumber", string.IsNullOrEmpty(licenseNumber) ? (object)DBNull.Value : licenseNumber);
                    cmd.Parameters.AddWithValue("@licenseDate", licenseDate.HasValue ? (object)licenseDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@accessLevel", accessLevel);

                    // Параметр для фото
                    if (photoData != null && photoData.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@photo", photoData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkLicense_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkLicense.Checked;
            txtLicenseNumber.Enabled = enabled;
            chkHasLicenseDate.Enabled = enabled;
            dtpLicenseDate.Enabled = enabled && chkHasLicenseDate.Checked;

            if (!enabled)
            {
                txtLicenseNumber.Text = "";
                chkHasLicenseDate.Checked = false;
            }
        }

        private void chkHasLicenseDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpLicenseDate.Enabled = chkHasLicenseDate.Checked;
            if (!chkHasLicenseDate.Checked)
            {
                dtpLicenseDate.Value = DateTime.Today;
            }
        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите фотографию сотрудника";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;

                        // Проверяем размер файла (макс 5 МБ)
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo.Length > 5 * 1024 * 1024) // 5 МБ
                        {
                            MessageBox.Show("Размер файла не должен превышать 5 МБ",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Загружаем изображение
                        photoData = File.ReadAllBytes(filePath);
                        DisplayPhoto();

                        MessageBox.Show("Фотография загружена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки фотографии:\n{ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeletePhoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить фотографию сотрудника?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                photoData = null;
                pbPhoto.Image = null;
            }
        }
    }
}