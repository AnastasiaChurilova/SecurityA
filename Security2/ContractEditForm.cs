using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;

namespace Security2
{
    public partial class ContractEditForm : Form
    {
        private string connectionString;
        private int contractId;
        private bool isEditMode;

        // Для хранения данных выпадающих списков
        private Dictionary<int, string> clients = new Dictionary<int, string>();
        private Dictionary<int, string> objects = new Dictionary<int, string>();
        private Dictionary<int, string> services = new Dictionary<int, string>();
        private Dictionary<int, string> employees = new Dictionary<int, string>();

        public ContractEditForm(string connectionString, int contractId = 0)
        {
            this.connectionString = connectionString;
            this.contractId = contractId;
            this.isEditMode = contractId > 0;

            InitializeComponent();

            try
            {
                LoadComboBoxData();

                if (isEditMode)
                {
                    LoadContractData();
                    this.Text = "Редактирование договора №" + contractId;
                }
                else
                {
                    this.Text = "Добавление нового договора";
                    dtpOrderDate.Value = DateTime.Today;

                    // Безопасная установка SelectedIndex
                    if (cmbStatus.Items.Count > 0)
                        cmbStatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации формы: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    // ЗАГРУЗКА КЛИЕНТОВ - только наименования
                    string sqlClients = "SELECT \"Наименование_или_ФИО\" FROM \"Заказчики\" ORDER BY \"Наименование_или_ФИО\"";
                    using (var cmd = new FbCommand(sqlClients, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbClient.Items.Clear();
                        cmbClient.Items.Add("-- Выберите клиента --");

                        // Загружаем всех клиентов
                        while (reader.Read())
                        {
                            cmbClient.Items.Add(reader.GetString(0));
                        }

                        // Устанавливаем первый элемент выбранным
                        if (cmbClient.Items.Count > 0)
                            cmbClient.SelectedIndex = 0;
                    }

                    // ЗАГРУЗКА ОБЪЕКТОВ - только активные объекты
                    string sqlObjects = @"
                SELECT DISTINCT ""Адрес_объекта"" 
                FROM ""Охраняемые_объекты"" 
                WHERE ""Статус"" = 'Активен' 
                ORDER BY ""Адрес_объекта""";

                    using (var cmd = new FbCommand(sqlObjects, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbObject.Items.Clear();
                        cmbObject.Items.Add("-- Выберите объект --");

                        // Загружаем активные объекты
                        while (reader.Read())
                        {
                            cmbObject.Items.Add(reader.GetString(0));
                        }

                        // Устанавливаем первый элемент выбранным
                        if (cmbObject.Items.Count > 0)
                            cmbObject.SelectedIndex = 0;
                    }

                    // ЗАГРУЗКА УСЛУГ - только наименования
                    string sqlServices = "SELECT \"Наименование\" FROM \"Услуги\" ORDER BY \"Наименование\"";
                    using (var cmd = new FbCommand(sqlServices, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbService.Items.Clear();
                        cmbService.Items.Add("-- Выберите услугу --");

                        while (reader.Read())
                        {
                            cmbService.Items.Add(reader.GetString(0));
                        }

                        if (cmbService.Items.Count > 0)
                            cmbService.SelectedIndex = 0;
                    }

                    // ЗАГРУЗКА СОТРУДНИКОВ - только менеджеры и администраторы
                    string sqlEmployees = @"
                SELECT DISTINCT ""ФИО"" 
                FROM ""Сотрудники"" 
                WHERE (
                    UPPER(""Должность"") LIKE '%МЕНЕДЖЕР%' 
                    OR UPPER(""Должность"") LIKE '%АДМИНИСТРАТОР%' 
                    OR UPPER(""Должность"") LIKE '%РУКОВОДИТЕЛЬ%'
                )
                ORDER BY ""ФИО""";

                    using (var cmd = new FbCommand(sqlEmployees, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbEmployee.Items.Clear();
                        cmbEmployee.Items.Add("-- Выберите ответственного --");

                        while (reader.Read())
                        {
                            cmbEmployee.Items.Add(reader.GetString(0));
                        }

                        if (cmbEmployee.Items.Count > 0)
                            cmbEmployee.SelectedIndex = 0;
                    }

                    // СТАТУСЫ
                    cmbStatus.Items.Clear();
                    cmbStatus.Items.AddRange(new string[] { "Активен", "Завершён", "Приостановлен" });
                    if (cmbStatus.Items.Count > 0)
                        cmbStatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Закрываем форму при ошибке загрузки данных
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LoadContractData()
        {
            try
            {
                string sql = @"
            SELECT 
                z.""Наименование_или_ФИО"",
                o.""Адрес_объекта"",
                u.""Наименование"",
                s.""ФИО"",
                d.""Дата_заказа"",
                d.""Дата_выполнения"",
                d.""Статус"",
                d.""Сумма""
            FROM ""Договоры"" d
            LEFT JOIN ""Заказчики"" z ON d.""ID_заказчика"" = z.""ID_заказчика""
            LEFT JOIN ""Охраняемые_объекты"" o ON d.""ID_объекта"" = o.""ID_объекта""
            LEFT JOIN ""Услуги"" u ON d.""ID_услуги"" = u.""ID_услуги""
            LEFT JOIN ""Сотрудники"" s ON d.""ID_сотрудника"" = s.""ID_сотрудника""
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
                                // Устанавливаем значения по наименованиям
                                SetComboBoxValue(cmbClient, reader.GetString(0));
                                SetComboBoxValue(cmbObject, reader.GetString(1));
                                SetComboBoxValue(cmbService, reader.GetString(2));
                                SetComboBoxValue(cmbEmployee, reader.GetString(3));

                                dtpOrderDate.Value = reader.GetDateTime(4);

                                if (!reader.IsDBNull(5))
                                {
                                    dtpCompletionDate.Value = reader.GetDateTime(5);
                                    chkHasCompletionDate.Checked = true;
                                }
                                else
                                {
                                    chkHasCompletionDate.Checked = false;
                                }

                                // Устанавливаем статус
                                string status = reader.GetString(6);
                                for (int i = 0; i < cmbStatus.Items.Count; i++)
                                {
                                    if (cmbStatus.Items[i].ToString() == status)
                                    {
                                        cmbStatus.SelectedIndex = i;
                                        break;
                                    }
                                }

                                txtAmount.Text = reader.GetDecimal(7).ToString("F2");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных договора:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            // Пытаемся найти в списке
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == value)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            // Если не нашли в списке, устанавливаем как текст (для объектов)
            comboBox.Text = value;
        }

        private void SafeSetComboBox(ComboBox comboBox, int id, Dictionary<int, string> dictionary)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                string itemText = comboBox.Items[i].ToString();
                if (itemText.StartsWith($"{id}:"))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            // Если не нашли, выбираем первый элемент (если он есть)
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        private void SetComboBoxValue(ComboBox comboBox, int id, Dictionary<int, string> dictionary)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().StartsWith($"{id}:"))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            comboBox.SelectedIndex = 0;
        }

        private int GetSelectedId(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex <= 0) return 0;

            string item = comboBox.Items[comboBox.SelectedIndex].ToString();
            if (item.Contains(":"))
            {
                string idPart = item.Split(':')[0].Trim();
                if (int.TryParse(idPart, out int id))
                    return id;
            }
            return 0;
        }

        private bool ValidateForm()
        {
            // Проверяем, что выбраны значения (не первый элемент с "--")
            if (cmbClient.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClient.Focus();
                return false;
            }

            if (cmbObject.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите объект", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbObject.Focus();
                return false;
            }

            if (cmbService.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbService.Focus();
                return false;
            }

            if (cmbEmployee.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите ответственного сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEmployee.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму договора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmount.Focus();
                return false;
            }

            if (chkHasCompletionDate.Checked && dtpCompletionDate.Value < dtpOrderDate.Value)
            {
                MessageBox.Show("Дата выполнения не может быть раньше даты заказа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpCompletionDate.Focus();
                return false;
            }

            return true;
        }

        private int GetClientId(string clientName)
        {
            if (string.IsNullOrEmpty(clientName) || clientName == "-- Выберите клиента --")
                throw new Exception("Выберите клиента");

            string sql = "SELECT \"ID_заказчика\" FROM \"Заказчики\" WHERE \"Наименование_или_ФИО\" = @name";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", clientName);
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        throw new Exception($"Клиент '{clientName}' не найден в базе данных");

                    return Convert.ToInt32(result);
                }
            }
        }

        private int GetObjectId(string objectAddress)
        {
            if (string.IsNullOrEmpty(objectAddress) || objectAddress == "-- Выберите объект --")
                throw new Exception("Выберите объект");

            // Ищем существующий объект
            string findSql = "SELECT \"ID_объекта\" FROM \"Охраняемые_объекты\" WHERE \"Адрес_объекта\" = @address";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(findSql, connection))
                {
                    cmd.Parameters.AddWithValue("@address", objectAddress);
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        throw new Exception($"Объект '{objectAddress}' не найден в базе данных");

                    return Convert.ToInt32(result);
                }
            }
        }

        private int GetServiceId(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName) || serviceName == "-- Выберите услугу --")
                throw new Exception("Выберите услугу");

            string sql = "SELECT \"ID_услуги\" FROM \"Услуги\" WHERE \"Наименование\" = @name";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", serviceName);
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        throw new Exception($"Услуга '{serviceName}' не найдена в базе данных");

                    return Convert.ToInt32(result);
                }
            }
        }

        private int GetEmployeeId(string employeeName)
        {
            if (string.IsNullOrEmpty(employeeName) || employeeName == "-- Выберите ответственного --")
                throw new Exception("Выберите ответственного сотрудника");

            string sql = "SELECT \"ID_сотрудника\" FROM \"Сотрудники\" WHERE \"ФИО\" = @name";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", employeeName);
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        throw new Exception($"Сотрудник '{employeeName}' не найден в базе данных");

                    return Convert.ToInt32(result);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                // Получаем ID из наименований
                int clientId = GetClientId(cmbClient.Text);
                int objectId = GetObjectId(cmbObject.Text); // Используем GetObjectId вместо GetOrCreateObjectId
                int serviceId = GetServiceId(cmbService.Text);
                int employeeId = GetEmployeeId(cmbEmployee.Text);

                DateTime orderDate = dtpOrderDate.Value;
                DateTime? completionDate = chkHasCompletionDate.Checked ? (DateTime?)dtpCompletionDate.Value : null;
                string status = cmbStatus.Text;
                decimal amount = decimal.Parse(txtAmount.Text);

                if (isEditMode)
                {
                    UpdateContract(clientId, objectId, serviceId, employeeId, orderDate, completionDate, status, amount);
                }
                else
                {
                    InsertContract(clientId, objectId, serviceId, employeeId, orderDate, completionDate, status, amount);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения договора:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertContract(int clientId, int objectId, int serviceId, int employeeId,
                                    DateTime orderDate, DateTime? completionDate, string status, decimal amount)
        {
            string sql = @"
                INSERT INTO ""Договоры"" (
                    ""ID_заказчика"",
                    ""ID_объекта"",
                    ""ID_услуги"",
                    ""ID_сотрудника"",
                    ""Дата_заказа"",
                    ""Дата_выполнения"",
                    ""Статус"",
                    ""Сумма""
                ) VALUES (
                    @clientId, @objectId, @serviceId, @employeeId, 
                    @orderDate, @completionDate, @status, @amount
                )";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@objectId", objectId);
                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@orderDate", orderDate);
                    cmd.Parameters.AddWithValue("@completionDate", completionDate.HasValue ? (object)completionDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateContract(int clientId, int objectId, int serviceId, int employeeId,
                                    DateTime orderDate, DateTime? completionDate, string status, decimal amount)
        {
            string sql = @"
                UPDATE ""Договоры"" SET
                    ""ID_заказчика"" = @clientId,
                    ""ID_объекта"" = @objectId,
                    ""ID_услуги"" = @serviceId,
                    ""ID_сотрудника"" = @employeeId,
                    ""Дата_заказа"" = @orderDate,
                    ""Дата_выполнения"" = @completionDate,
                    ""Статус"" = @status,
                    ""Сумма"" = @amount
                WHERE ""ID_договора"" = @id";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new FbCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", contractId);
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@objectId", objectId);
                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@orderDate", orderDate);
                    cmd.Parameters.AddWithValue("@completionDate", completionDate.HasValue ? (object)completionDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkHasCompletionDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpCompletionDate.Enabled = chkHasCompletionDate.Checked;
            if (!chkHasCompletionDate.Checked)
            {
                dtpCompletionDate.Value = DateTime.Today;
            }
        }

        private void cmbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Автоматически подставляем стоимость услуги
            if (cmbService.SelectedIndex > 0 && string.IsNullOrEmpty(txtAmount.Text))
            {
                try
                {
                    string serviceName = cmbService.Text;
                    string sql = "SELECT \"Стоимость\" FROM \"Услуги\" WHERE \"Наименование\" = @name";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", serviceName);
                            var result = cmd.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                txtAmount.Text = Convert.ToDecimal(result).ToString("F2");
                            }
                        }
                    }
                }
                catch { /* Игнорируем ошибки автозаполнения */ }
            }
        }
        private void ContractEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}