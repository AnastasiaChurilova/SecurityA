using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Security2
{
    public partial class IncidentEditForm : Form
    {
        private string connectionString;
        private int incidentId;
        private int? currentEmployeeId; // Храним ID текущего сотрудника

        public IncidentEditForm(string connectionString, int incidentId)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.incidentId = incidentId;

            LoadObjects();

            // Подписываемся на событие изменения выбора объекта
            cmbObject.SelectedIndexChanged += CmbObject_SelectedIndexChanged;

            if (incidentId > 0)
                LoadIncidentData();
            else
                UpdateResponsibleInfo(); // Показываем информацию по умолчанию
        }

        private void CmbObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Автоматически обновляем информацию об ответственном при выборе объекта
            UpdateResponsibleInfo();
        }

        private void LoadObjects()
        {
            try
            {
                string sql = "SELECT \"ID_объекта\", \"Адрес_объекта\" FROM \"Охраняемые_объекты\" ORDER BY \"Адрес_объекта\"";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbObject.Items.Add(new ComboBoxItem(0, "Не выбран"));
                        while (reader.Read())
                        {
                            cmbObject.Items.Add(new ComboBoxItem(
                                reader.GetInt32(0),
                                reader.GetString(1)
                            ));
                        }
                    }
                }

                if (cmbObject.Items.Count > 0)
                    cmbObject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке объектов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateResponsibleInfo()
        {
            if (cmbObject.SelectedItem is ComboBoxItem selectedObject)
            {
                if (selectedObject.Id > 0)
                {
                    LoadResponsibleEmployee(selectedObject.Id);
                }
                else
                {
                    // Если объект не выбран
                    currentEmployeeId = null;
                    lblResponsible.Text = "Не назначен";
                    lblResponsible.ForeColor = Color.Gray;
                    lblContractInfo.Text = "Объект не выбран";
                    lblContractInfo.ForeColor = Color.Gray;
                }
            }
        }

        private void LoadResponsibleEmployee(int objectId)
        {
            try
            {
                // Ищем активный договор для выбранного объекта
                string sql = @"
                    SELECT 
                        d.""ID_сотрудника"", 
                        s.""ФИО"",
                        d.""ID_договора"",
                        d.""Дата_заказа"",
                        d.""Дата_выполнения"",
                        d.""Статус""
                    FROM ""Договоры"" d
                    INNER JOIN ""Сотрудники"" s ON d.""ID_сотрудника"" = s.""ID_сотрудника""
                    WHERE d.""ID_объекта"" = @objectId 
                      AND d.""Статус"" = 'Активен'
                    ORDER BY d.""Дата_заказа"" DESC
                    ROWS 1";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@objectId", objectId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int employeeId = reader.GetInt32(0);
                                string employeeName = reader.GetString(1);
                                int contractId = reader.GetInt32(2);
                                DateTime orderDate = reader.GetDateTime(3);
                                DateTime? completionDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                                string status = reader.GetString(5);

                                // Сохраняем ID сотрудника
                                currentEmployeeId = employeeId;

                                // Обновляем информацию об ответственном
                                lblResponsible.Text = employeeName;
                                lblResponsible.ForeColor = Color.DarkGreen;

                                // Показываем информацию о договоре
                                lblContractInfo.Text = $"Договор №{contractId} от {orderDate:dd.MM.yyyy}";
                                lblContractInfo.ForeColor = Color.DarkBlue;

                                // Информация о сроках
                                if (completionDate.HasValue)
                                {
                                    lblContractInfo.Text += $" до {completionDate.Value:dd.MM.yyyy}";
                                }
                            }
                            else
                            {
                                // Если активного договора нет
                                currentEmployeeId = null;
                                lblResponsible.Text = "Не назначен";
                                lblResponsible.ForeColor = Color.Red;
                                lblContractInfo.Text = "Активный договор не найден";
                                lblContractInfo.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                currentEmployeeId = null;
                lblResponsible.Text = "Ошибка загрузки";
                lblResponsible.ForeColor = Color.Red;
                lblContractInfo.Text = $"Ошибка: {ex.Message}";
                lblContractInfo.ForeColor = Color.Red;
            }
        }

        private void LoadIncidentData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ""Тип_инцидента"",
                        ""Описание"",
                        ""Принятые_меры"",
                        ""Статус"",
                        ""ID_объекта"",
                        ""ID_сотрудника""
                    FROM ""Инциденты""
                    WHERE ""ID_инцидента"" = @id";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", incidentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtType.Text = reader.GetString(0);
                                txtDescription.Text = reader.GetString(1);
                                txtMeasures.Text = reader.GetString(2);
                                txtStatus.Text = reader.GetString(3);

                                // Выбираем объект
                                if (!reader.IsDBNull(4))
                                {
                                    int objectId = reader.GetInt32(4);
                                    SelectComboBoxItem(cmbObject, objectId);

                                    // Проверяем, совпадает ли сохраненный сотрудник с текущим
                                    if (!reader.IsDBNull(5))
                                    {
                                        int savedEmployeeId = reader.GetInt32(5);

                                        // Если сохраненный сотрудник отличается от текущего,
                                        // показываем предупреждение
                                        if (currentEmployeeId.HasValue && currentEmployeeId.Value != savedEmployeeId)
                                        {
                                            MessageBox.Show("ВНИМАНИЕ: Ответственный сотрудник был изменен в договоре после создания инцидента!",
                                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                                else
                                {
                                    UpdateResponsibleInfo(); // Показываем информацию по умолчанию
                                }
                            }
                            else
                            {
                                MessageBox.Show("Инцидент не найден", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных инцидента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SelectComboBoxItem(ComboBox comboBox, int id)
        {
            if (id <= 0)
            {
                comboBox.SelectedIndex = 0;
                return true;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                ComboBoxItem item = comboBox.Items[i] as ComboBoxItem;
                if (item != null && item.Id == id)
                {
                    comboBox.SelectedIndex = i;
                    return true;
                }
            }

            // Если не найден, выбираем первый элемент
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Введите тип инцидента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return;
            }

            try
            {
                int? objectId = (cmbObject.SelectedItem as ComboBoxItem)?.Id;

                // Проверка: если объект выбран, но нет активного договора
                if (objectId.HasValue && objectId > 0 && !currentEmployeeId.HasValue)
                {
                    var result = MessageBox.Show("Для выбранного объекта нет активного договора. Ответственный не назначен. Продолжить сохранение?",
                        "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                if (incidentId == 0) // Добавление
                {
                    string sql = @"
                        INSERT INTO ""Инциденты"" (
                            ""Тип_инцидента"",
                            ""Описание"",
                            ""Принятые_меры"",
                            ""Статус"",
                            ""ID_объекта"",
                            ""ID_сотрудника"",
                            ""Дата_время""
                        ) VALUES (
                            @type, @desc, @measures, @status, @objectId, @employeeId, CURRENT_TIMESTAMP
                        )";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@type", txtType.Text);
                            cmd.Parameters.AddWithValue("@desc", txtDescription.Text ?? "");
                            cmd.Parameters.AddWithValue("@measures", txtMeasures.Text ?? "");
                            cmd.Parameters.AddWithValue("@status", txtStatus.Text ?? "Открыт");
                            cmd.Parameters.AddWithValue("@objectId",
                                objectId.HasValue && objectId > 0 ? (object)objectId.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@employeeId",
                                currentEmployeeId.HasValue ? (object)currentEmployeeId.Value : DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else // Редактирование
                {
                    string sql = @"
                        UPDATE ""Инциденты"" SET
                            ""Тип_инцидента"" = @type,
                            ""Описание"" = @desc,
                            ""Принятые_меры"" = @measures,
                            ""Статус"" = @status,
                            ""ID_объекта"" = @objectId,
                            ""ID_сотрудника"" = @employeeId
                        WHERE ""ID_инцидента"" = @id";

                    using (var connection = new FbConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new FbCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@type", txtType.Text);
                            cmd.Parameters.AddWithValue("@desc", txtDescription.Text ?? "");
                            cmd.Parameters.AddWithValue("@measures", txtMeasures.Text ?? "");
                            cmd.Parameters.AddWithValue("@status", txtStatus.Text ?? "Открыт");
                            cmd.Parameters.AddWithValue("@objectId",
                                objectId.HasValue && objectId > 0 ? (object)objectId.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@employeeId",
                                currentEmployeeId.HasValue ? (object)currentEmployeeId.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@id", incidentId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Вспомогательный класс для ComboBox
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public ComboBoxItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}