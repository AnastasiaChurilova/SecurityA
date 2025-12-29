using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Security2
{
    public partial class ReportsForm : Form
    {
        private string connectionString;
        private DataTable revenueData;
        private DataTable contractsData;
        private DataTable incidentsData;

        public ReportsForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            // Инициализация фильтров по датам
            dtpRevenueFrom.Value = DateTime.Today.AddMonths(-3);
            dtpRevenueTo.Value = DateTime.Today;
            dtpContractsFrom.Value = DateTime.Today.AddMonths(-6);
            dtpContractsTo.Value = DateTime.Today;
            dtpIncidentsFrom.Value = DateTime.Today.AddMonths(-1);
            dtpIncidentsTo.Value = DateTime.Today;

            // Настраиваем DataGridView перед загрузкой данных
            ConfigureGrids();

            // Загружаем отчеты
            LoadAllReports();
        }

        private void ConfigureGrids()
        {
            // Настраиваем DataGridView для выручки
            dgvRevenue.AutoGenerateColumns = false;
            dgvRevenue.Columns.Clear();

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PaymentDate",
                DataPropertyName = "PaymentDate",
                HeaderText = "Дата",
                Width = 100
            });

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DailyRevenue",
                DataPropertyName = "DailyRevenue",
                HeaderText = "Выручка за день",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PaymentCount",
                DataPropertyName = "PaymentCount",
                HeaderText = "Кол-во платежей",
                Width = 120
            });

            // Настраиваем DataGridView для договоров
            dgvContracts.AutoGenerateColumns = false;
            dgvContracts.Columns.Clear();

            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Width = 120
            });

            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ContractCount",
                DataPropertyName = "ContractCount",
                HeaderText = "Количество",
                Width = 100
            });

            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TotalAmount",
                DataPropertyName = "TotalAmount",
                HeaderText = "Общая сумма",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "AvgAmount",
                DataPropertyName = "AvgAmount",
                HeaderText = "Средняя сумма",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Настраиваем DataGridView для инцидентов
            dgvIncidents.AutoGenerateColumns = false;
            dgvIncidents.Columns.Clear();

            dgvIncidents.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "IncidentType",
                DataPropertyName = "IncidentType",
                HeaderText = "Тип инцидента",
                Width = 150
            });

            dgvIncidents.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "IncidentCount",
                DataPropertyName = "IncidentCount",
                HeaderText = "Количество",
                Width = 100
            });

            dgvIncidents.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Width = 100
            });

            dgvIncidents.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "IncidentDate",
                DataPropertyName = "IncidentDate",
                HeaderText = "Дата",
                Width = 100
            });
        }

        private void LoadAllReports()
        {
            LoadRevenueReport();
            LoadContractsReport();
            LoadIncidentsReport();
            UpdateSummary();
        }

        #region Выручка
        private void LoadRevenueReport()
        {
            string sql = "";

            try
            {
                sql = @"SELECT 
                    CAST(p.""Дата_платежа"" AS DATE) as PaymentDate,
                    SUM(p.""Сумма"") as DailyRevenue,
                    COUNT(*) as PaymentCount
                FROM ""Платежи"" p
                WHERE p.""Статус"" = 'Оплачено'
                  AND p.""Дата_платежа"" BETWEEN @from AND @to
                GROUP BY CAST(p.""Дата_платежа"" AS DATE)
                ORDER BY PaymentDate DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpRevenueFrom.Value);
                        cmd.Parameters.AddWithValue("@to", dtpRevenueTo.Value.AddDays(1).AddSeconds(-1));

                        var adapter = new FbDataAdapter(cmd);
                        revenueData = new DataTable();
                        adapter.Fill(revenueData);

                        // Форматируем дату
                        foreach (DataRow row in revenueData.Rows)
                        {
                            if (row["PaymentDate"] != DBNull.Value)
                            {
                                row["PaymentDate"] = ((DateTime)row["PaymentDate"]).ToString("dd.MM.yyyy");
                            }
                        }

                        dgvRevenue.DataSource = revenueData;
                        CalculateRevenueTotals();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчета по выручке: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateRevenueTotals()
        {
            if (revenueData == null || revenueData.Rows.Count == 0)
            {
                lblRevenueTotal.Text = "Всего: 0 ₽";
                lblRevenueAvg.Text = "Среднедневная: 0 ₽";
                return;
            }

            decimal total = 0;
            int days = revenueData.Rows.Count;

            foreach (DataRow row in revenueData.Rows)
            {
                if (row["DailyRevenue"] != DBNull.Value)
                    total += Convert.ToDecimal(row["DailyRevenue"]);
            }

            lblRevenueTotal.Text = $"Всего: {total:C2}";
            lblRevenueAvg.Text = $"Среднедневная: {total / days:C2}";
        }

        private void btnRevenueRefresh_Click(object sender, EventArgs e)
        {
            LoadRevenueReport();
        }
        #endregion

        #region Договоры
        private void LoadContractsReport()
        {
            string sql = "";

            try
            {
                sql = @"SELECT 
                    c.""Статус"" as Status,
                    COUNT(*) as ContractCount,
                    SUM(c.""Сумма"") as TotalAmount,
                    AVG(c.""Сумма"") as AvgAmount
                FROM ""Договоры"" c
                WHERE c.""Дата_заказа"" BETWEEN @from AND @to
                GROUP BY c.""Статус""
                ORDER BY ContractCount DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpContractsFrom.Value);
                        cmd.Parameters.AddWithValue("@to", dtpContractsTo.Value.AddDays(1));

                        var adapter = new FbDataAdapter(cmd);
                        contractsData = new DataTable();
                        adapter.Fill(contractsData);

                        dgvContracts.DataSource = contractsData;
                        CalculateContractsTotals();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчета по договорам: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateContractsTotals()
        {
            if (contractsData == null || contractsData.Rows.Count == 0)
            {
                lblContractsTotal.Text = "Всего договоров: 0";
                lblContractsAmount.Text = "Общая сумма: 0 ₽";
                return;
            }

            int totalCount = 0;
            decimal totalAmount = 0;

            foreach (DataRow row in contractsData.Rows)
            {
                if (row["ContractCount"] != DBNull.Value)
                    totalCount += Convert.ToInt32(row["ContractCount"]);

                if (row["TotalAmount"] != DBNull.Value)
                    totalAmount += Convert.ToDecimal(row["TotalAmount"]);
            }

            lblContractsTotal.Text = $"Всего договоров: {totalCount}";
            lblContractsAmount.Text = $"Общая сумма: {totalAmount:C2}";
        }

        private void btnContractsRefresh_Click(object sender, EventArgs e)
        {
            LoadContractsReport();
        }
        #endregion

        #region Инциденты (вызовы охраны)
        private void LoadIncidentsReport()
        {
            string sql = "";

            try
            {
                sql = @"SELECT 
                    i.""Тип_инцидента"" as IncidentType,
                    COUNT(*) as IncidentCount,
                    i.""Статус"" as Status,
                    CAST(i.""Дата_время"" AS DATE) as IncidentDate
                FROM ""Инциденты"" i
                WHERE i.""Дата_время"" BETWEEN @from AND @to
                GROUP BY i.""Тип_инцидента"", i.""Статус"", CAST(i.""Дата_время"" AS DATE)
                ORDER BY IncidentDate DESC";

                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new FbCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpIncidentsFrom.Value);
                        cmd.Parameters.AddWithValue("@to", dtpIncidentsTo.Value.AddDays(1).AddSeconds(-1));

                        var adapter = new FbDataAdapter(cmd);
                        incidentsData = new DataTable();
                        adapter.Fill(incidentsData);

                        // Форматируем дату
                        foreach (DataRow row in incidentsData.Rows)
                        {
                            if (row["IncidentDate"] != DBNull.Value)
                            {
                                row["IncidentDate"] = ((DateTime)row["IncidentDate"]).ToString("dd.MM.yyyy");
                            }
                        }

                        dgvIncidents.DataSource = incidentsData;
                        CalculateIncidentsTotals();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчета по инцидентам: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateIncidentsTotals()
        {
            if (incidentsData == null || incidentsData.Rows.Count == 0)
            {
                lblIncidentsTotal.Text = "Всего инцидентов: 0";
                return;
            }

            int total = 0;
            foreach (DataRow row in incidentsData.Rows)
            {
                if (row["IncidentCount"] != DBNull.Value)
                    total += Convert.ToInt32(row["IncidentCount"]);
            }

            lblIncidentsTotal.Text = $"Всего инцидентов: {total}";
        }

        private void btnIncidentsRefresh_Click(object sender, EventArgs e)
        {
            LoadIncidentsReport();
        }
        #endregion

        #region Общая сводка
        private void UpdateSummary()
        {
            try
            {
                using (var connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    // 1. Общая выручка за последние 30 дней
                    string revenueSql = @"
                        SELECT COALESCE(SUM(""Сумма""), 0) 
                        FROM ""Платежи"" 
                        WHERE ""Статус"" = 'Оплачено' 
                          AND ""Дата_платежа"" >= CURRENT_DATE - 30";

                    using (var cmd = new FbCommand(revenueSql, connection))
                    {
                        decimal monthlyRevenue = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblMonthlyRevenue.Text = $"Выручка за месяц: {monthlyRevenue:C2}";
                    }

                    // 2. Активные договоры
                    string activeContractsSql = @"
                        SELECT COUNT(*) FROM ""Договоры"" WHERE ""Статус"" = 'Активен'";

                    using (var cmd = new FbCommand(activeContractsSql, connection))
                    {
                        int activeContracts = Convert.ToInt32(cmd.ExecuteScalar());
                        lblActiveContracts.Text = $"Активных договоров: {activeContracts}";
                    }

                    // 3. Все сотрудники
                    string employeesSql = @"
                        SELECT COUNT(*) FROM ""Сотрудники""";

                    using (var cmd = new FbCommand(employeesSql, connection))
                    {
                        int employees = Convert.ToInt32(cmd.ExecuteScalar());
                        lblEmployeesCount.Text = $"Всего сотрудников: {employees}";
                    }

                    // 4. Инциденты за последние 30 дней
                    string incidentsSql = @"
                        SELECT COUNT(*) FROM ""Инциденты"" 
                        WHERE ""Дата_время"" >= CURRENT_DATE - 30";

                    using (var cmd = new FbCommand(incidentsSql, connection))
                    {
                        int incidents = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMonthlyIncidents.Text = $"Инцидентов за месяц: {incidents}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сводки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Экспорт
        private void btnExportRevenue_Click(object sender, EventArgs e)
        {
            ExportToExcel(revenueData, "Отчет_по_выручке");
        }

        private void btnExportContracts_Click(object sender, EventArgs e)
        {
            ExportToExcel(contractsData, "Отчет_по_договорам");
        }

        private void btnExportIncidents_Click(object sender, EventArgs e)
        {
            ExportToExcel(incidentsData, "Отчет_по_инцидентам");
        }

        private void ExportToExcel(DataTable data, string fileName)
        {
            if (data == null || data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv";
                saveFileDialog.FileName = $"{fileName}_{DateTime.Now:yyyyMMdd_HHmm}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName, false,
                               System.Text.Encoding.UTF8))
                    {
                        // Записываем заголовки с русскими названиями
                        List<string> headers = new List<string>();
                        foreach (DataGridViewColumn column in GetGridByDataTable(data).Columns)
                        {
                            headers.Add(column.HeaderText);
                        }
                        writer.WriteLine(string.Join(";", headers));

                        // Записываем данные
                        foreach (DataRow row in data.Rows)
                        {
                            List<string> values = new List<string>();
                            for (int i = 0; i < data.Columns.Count; i++)
                            {
                                values.Add(row[i]?.ToString() ?? "");
                            }
                            writer.WriteLine(string.Join(";", values));
                        }
                    }

                    MessageBox.Show($"Данные экспортированы в файл:\n{saveFileDialog.FileName}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView GetGridByDataTable(DataTable data)
        {
            if (data == revenueData) return dgvRevenue;
            if (data == contractsData) return dgvContracts;
            if (data == incidentsData) return dgvIncidents;
            return null;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            LoadAllReports();
        }
    }
}