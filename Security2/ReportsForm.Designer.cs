using System.Windows.Forms;

namespace Security2
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabControl1;
        private TabPage tabRevenue;
        private TabPage tabContracts;
        private TabPage tabIncidents;

        // Revenue Tab
        private Panel panelRevenueFilter;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpRevenueFrom;
        private DateTimePicker dtpRevenueTo;
        private Button btnRevenueRefresh;
        private DataGridView dgvRevenue;
        private Label lblRevenueTotal;
        private Label lblRevenueAvg;
        private Button btnExportRevenue;

        // Contracts Tab
        private Panel panelContractsFilter;
        private Label label3;
        private Label label4;
        private DateTimePicker dtpContractsFrom;
        private DateTimePicker dtpContractsTo;
        private Button btnContractsRefresh;
        private DataGridView dgvContracts;
        private Label lblContractsTotal;
        private Label lblContractsAmount;
        private Button btnExportContracts;

        // Employees Tab
        private Panel panelEmployees;
        private Button btnEmployeesRefresh;
        private DataGridView dgvEmployees;
        private Button btnExportEmployees;

        // Incidents Tab
        private Panel panelIncidentsFilter;
        private Label label5;
        private Label label6;
        private DateTimePicker dtpIncidentsFrom;
        private DateTimePicker dtpIncidentsTo;
        private Button btnIncidentsRefresh;
        private DataGridView dgvIncidents;
        private Label lblIncidentsTotal;
        private Button btnExportIncidents;

        // Summary Panel
        private Panel panelSummary;
        private Label lblMonthlyRevenue;
        private Label lblActiveContracts;
        private Label lblEmployeesCount;
        private Label lblMonthlyIncidents;

        // Bottom Panel
        private Panel panelBottom;
        private Button btnRefreshAll;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.panelRevenueFilter = new System.Windows.Forms.Panel();
            this.btnExportRevenue = new System.Windows.Forms.Button();
            this.lblRevenueAvg = new System.Windows.Forms.Label();
            this.lblRevenueTotal = new System.Windows.Forms.Label();
            this.btnRevenueRefresh = new System.Windows.Forms.Button();
            this.dtpRevenueTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpRevenueFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRevenue = new System.Windows.Forms.DataGridView();
            this.tabContracts = new System.Windows.Forms.TabPage();
            this.panelContractsFilter = new System.Windows.Forms.Panel();
            this.btnExportContracts = new System.Windows.Forms.Button();
            this.lblContractsAmount = new System.Windows.Forms.Label();
            this.lblContractsTotal = new System.Windows.Forms.Label();
            this.btnContractsRefresh = new System.Windows.Forms.Button();
            this.dtpContractsTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpContractsFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvContracts = new System.Windows.Forms.DataGridView();
            this.tabIncidents = new System.Windows.Forms.TabPage();
            this.panelIncidentsFilter = new System.Windows.Forms.Panel();
            this.btnExportIncidents = new System.Windows.Forms.Button();
            this.lblIncidentsTotal = new System.Windows.Forms.Label();
            this.btnIncidentsRefresh = new System.Windows.Forms.Button();
            this.dtpIncidentsTo = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpIncidentsFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvIncidents = new System.Windows.Forms.DataGridView();
            this.panelEmployees = new System.Windows.Forms.Panel();
            this.btnExportEmployees = new System.Windows.Forms.Button();
            this.btnEmployeesRefresh = new System.Windows.Forms.Button();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblMonthlyIncidents = new System.Windows.Forms.Label();
            this.lblEmployeesCount = new System.Windows.Forms.Label();
            this.lblActiveContracts = new System.Windows.Forms.Label();
            this.lblMonthlyRevenue = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnRefreshAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabRevenue.SuspendLayout();
            this.panelRevenueFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).BeginInit();
            this.tabContracts.SuspendLayout();
            this.panelContractsFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContracts)).BeginInit();
            this.tabIncidents.SuspendLayout();
            this.panelIncidentsFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidents)).BeginInit();
            this.panelEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRevenue);
            this.tabControl1.Controls.Add(this.tabContracts);
            this.tabControl1.Controls.Add(this.tabIncidents);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 80);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 551);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRevenue
            // 
            this.tabRevenue.Controls.Add(this.panelRevenueFilter);
            this.tabRevenue.Controls.Add(this.dgvRevenue);
            this.tabRevenue.Location = new System.Drawing.Point(4, 22);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(1076, 525);
            this.tabRevenue.TabIndex = 0;
            this.tabRevenue.Text = "Выручка";
            this.tabRevenue.UseVisualStyleBackColor = true;
            // 
            // panelRevenueFilter
            // 
            this.panelRevenueFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelRevenueFilter.Controls.Add(this.btnExportRevenue);
            this.panelRevenueFilter.Controls.Add(this.lblRevenueAvg);
            this.panelRevenueFilter.Controls.Add(this.lblRevenueTotal);
            this.panelRevenueFilter.Controls.Add(this.btnRevenueRefresh);
            this.panelRevenueFilter.Controls.Add(this.dtpRevenueTo);
            this.panelRevenueFilter.Controls.Add(this.label2);
            this.panelRevenueFilter.Controls.Add(this.dtpRevenueFrom);
            this.panelRevenueFilter.Controls.Add(this.label1);
            this.panelRevenueFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRevenueFilter.Location = new System.Drawing.Point(3, 3);
            this.panelRevenueFilter.Name = "panelRevenueFilter";
            this.panelRevenueFilter.Size = new System.Drawing.Size(1070, 60);
            this.panelRevenueFilter.TabIndex = 1;
            // 
            // btnExportRevenue
            // 
            this.btnExportRevenue.Location = new System.Drawing.Point(872, 17);
            this.btnExportRevenue.Name = "btnExportRevenue";
            this.btnExportRevenue.Size = new System.Drawing.Size(90, 26);
            this.btnExportRevenue.TabIndex = 7;
            this.btnExportRevenue.Text = "Экспорт";
            this.btnExportRevenue.UseVisualStyleBackColor = true;
            this.btnExportRevenue.Click += new System.EventHandler(this.btnExportRevenue_Click);
            // 
            // lblRevenueAvg
            // 
            this.lblRevenueAvg.AutoSize = true;
            this.lblRevenueAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRevenueAvg.Location = new System.Drawing.Point(615, 22);
            this.lblRevenueAvg.Name = "lblRevenueAvg";
            this.lblRevenueAvg.Size = new System.Drawing.Size(140, 15);
            this.lblRevenueAvg.TabIndex = 6;
            this.lblRevenueAvg.Text = "Среднедневная: 0 ₽";
            // 
            // lblRevenueTotal
            // 
            this.lblRevenueTotal.AutoSize = true;
            this.lblRevenueTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRevenueTotal.Location = new System.Drawing.Point(470, 22);
            this.lblRevenueTotal.Name = "lblRevenueTotal";
            this.lblRevenueTotal.Size = new System.Drawing.Size(73, 15);
            this.lblRevenueTotal.TabIndex = 5;
            this.lblRevenueTotal.Text = "Всего: 0 ₽";
            // 
            // btnRevenueRefresh
            // 
            this.btnRevenueRefresh.Location = new System.Drawing.Point(374, 17);
            this.btnRevenueRefresh.Name = "btnRevenueRefresh";
            this.btnRevenueRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnRevenueRefresh.TabIndex = 4;
            this.btnRevenueRefresh.Text = "Обновить";
            this.btnRevenueRefresh.UseVisualStyleBackColor = true;
            this.btnRevenueRefresh.Click += new System.EventHandler(this.btnRevenueRefresh_Click);
            // 
            // dtpRevenueTo
            // 
            this.dtpRevenueTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRevenueTo.Location = new System.Drawing.Point(265, 20);
            this.dtpRevenueTo.Name = "dtpRevenueTo";
            this.dtpRevenueTo.Size = new System.Drawing.Size(100, 20);
            this.dtpRevenueTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "по";
            // 
            // dtpRevenueFrom
            // 
            this.dtpRevenueFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRevenueFrom.Location = new System.Drawing.Point(134, 20);
            this.dtpRevenueFrom.Name = "dtpRevenueFrom";
            this.dtpRevenueFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpRevenueFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период выручки: с";
            // 
            // dgvRevenue
            // 
            this.dgvRevenue.AllowUserToAddRows = false;
            this.dgvRevenue.AllowUserToDeleteRows = false;
            this.dgvRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenue.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvRevenue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRevenue.Location = new System.Drawing.Point(3, 3);
            this.dgvRevenue.Name = "dgvRevenue";
            this.dgvRevenue.ReadOnly = true;
            this.dgvRevenue.Size = new System.Drawing.Size(1070, 519);
            this.dgvRevenue.TabIndex = 0;
            // 
            // tabContracts
            // 
            this.tabContracts.Controls.Add(this.panelContractsFilter);
            this.tabContracts.Controls.Add(this.dgvContracts);
            this.tabContracts.Location = new System.Drawing.Point(4, 22);
            this.tabContracts.Name = "tabContracts";
            this.tabContracts.Padding = new System.Windows.Forms.Padding(3);
            this.tabContracts.Size = new System.Drawing.Size(1076, 525);
            this.tabContracts.TabIndex = 1;
            this.tabContracts.Text = "Договоры";
            this.tabContracts.UseVisualStyleBackColor = true;
            // 
            // panelContractsFilter
            // 
            this.panelContractsFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelContractsFilter.Controls.Add(this.btnExportContracts);
            this.panelContractsFilter.Controls.Add(this.lblContractsAmount);
            this.panelContractsFilter.Controls.Add(this.lblContractsTotal);
            this.panelContractsFilter.Controls.Add(this.btnContractsRefresh);
            this.panelContractsFilter.Controls.Add(this.dtpContractsTo);
            this.panelContractsFilter.Controls.Add(this.label4);
            this.panelContractsFilter.Controls.Add(this.dtpContractsFrom);
            this.panelContractsFilter.Controls.Add(this.label3);
            this.panelContractsFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContractsFilter.Location = new System.Drawing.Point(3, 3);
            this.panelContractsFilter.Name = "panelContractsFilter";
            this.panelContractsFilter.Size = new System.Drawing.Size(1070, 60);
            this.panelContractsFilter.TabIndex = 2;
            // 
            // btnExportContracts
            // 
            this.btnExportContracts.Location = new System.Drawing.Point(872, 17);
            this.btnExportContracts.Name = "btnExportContracts";
            this.btnExportContracts.Size = new System.Drawing.Size(90, 26);
            this.btnExportContracts.TabIndex = 8;
            this.btnExportContracts.Text = "Экспорт";
            this.btnExportContracts.UseVisualStyleBackColor = true;
            this.btnExportContracts.Click += new System.EventHandler(this.btnExportContracts_Click);
            // 
            // lblContractsAmount
            // 
            this.lblContractsAmount.AutoSize = true;
            this.lblContractsAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblContractsAmount.Location = new System.Drawing.Point(615, 22);
            this.lblContractsAmount.Name = "lblContractsAmount";
            this.lblContractsAmount.Size = new System.Drawing.Size(112, 15);
            this.lblContractsAmount.TabIndex = 7;
            this.lblContractsAmount.Text = "Общая сумма: 0";
            // 
            // lblContractsTotal
            // 
            this.lblContractsTotal.AutoSize = true;
            this.lblContractsTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblContractsTotal.Location = new System.Drawing.Point(470, 22);
            this.lblContractsTotal.Name = "lblContractsTotal";
            this.lblContractsTotal.Size = new System.Drawing.Size(135, 15);
            this.lblContractsTotal.TabIndex = 6;
            this.lblContractsTotal.Text = "Всего договоров: 0";
            // 
            // btnContractsRefresh
            // 
            this.btnContractsRefresh.Location = new System.Drawing.Point(374, 17);
            this.btnContractsRefresh.Name = "btnContractsRefresh";
            this.btnContractsRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnContractsRefresh.TabIndex = 5;
            this.btnContractsRefresh.Text = "Обновить";
            this.btnContractsRefresh.UseVisualStyleBackColor = true;
            this.btnContractsRefresh.Click += new System.EventHandler(this.btnContractsRefresh_Click);
            // 
            // dtpContractsTo
            // 
            this.dtpContractsTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpContractsTo.Location = new System.Drawing.Point(265, 20);
            this.dtpContractsTo.Name = "dtpContractsTo";
            this.dtpContractsTo.Size = new System.Drawing.Size(100, 20);
            this.dtpContractsTo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "по";
            // 
            // dtpContractsFrom
            // 
            this.dtpContractsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpContractsFrom.Location = new System.Drawing.Point(134, 20);
            this.dtpContractsFrom.Name = "dtpContractsFrom";
            this.dtpContractsFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpContractsFrom.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Период договоров: с";
            // 
            // dgvContracts
            // 
            this.dgvContracts.AllowUserToAddRows = false;
            this.dgvContracts.AllowUserToDeleteRows = false;
            this.dgvContracts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContracts.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvContracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContracts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContracts.Location = new System.Drawing.Point(3, 3);
            this.dgvContracts.Name = "dgvContracts";
            this.dgvContracts.ReadOnly = true;
            this.dgvContracts.Size = new System.Drawing.Size(1070, 519);
            this.dgvContracts.TabIndex = 1;
            // 
            // tabIncidents
            // 
            this.tabIncidents.Controls.Add(this.panelIncidentsFilter);
            this.tabIncidents.Controls.Add(this.dgvIncidents);
            this.tabIncidents.Location = new System.Drawing.Point(4, 22);
            this.tabIncidents.Name = "tabIncidents";
            this.tabIncidents.Size = new System.Drawing.Size(1076, 525);
            this.tabIncidents.TabIndex = 3;
            this.tabIncidents.Text = "Инциденты";
            this.tabIncidents.UseVisualStyleBackColor = true;
            // 
            // panelIncidentsFilter
            // 
            this.panelIncidentsFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelIncidentsFilter.Controls.Add(this.btnExportIncidents);
            this.panelIncidentsFilter.Controls.Add(this.lblIncidentsTotal);
            this.panelIncidentsFilter.Controls.Add(this.btnIncidentsRefresh);
            this.panelIncidentsFilter.Controls.Add(this.dtpIncidentsTo);
            this.panelIncidentsFilter.Controls.Add(this.label6);
            this.panelIncidentsFilter.Controls.Add(this.dtpIncidentsFrom);
            this.panelIncidentsFilter.Controls.Add(this.label5);
            this.panelIncidentsFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelIncidentsFilter.Location = new System.Drawing.Point(0, 0);
            this.panelIncidentsFilter.Name = "panelIncidentsFilter";
            this.panelIncidentsFilter.Size = new System.Drawing.Size(1076, 60);
            this.panelIncidentsFilter.TabIndex = 2;
            // 
            // btnExportIncidents
            // 
            this.btnExportIncidents.Location = new System.Drawing.Point(872, 17);
            this.btnExportIncidents.Name = "btnExportIncidents";
            this.btnExportIncidents.Size = new System.Drawing.Size(90, 26);
            this.btnExportIncidents.TabIndex = 10;
            this.btnExportIncidents.Text = "Экспорт";
            this.btnExportIncidents.UseVisualStyleBackColor = true;
            this.btnExportIncidents.Click += new System.EventHandler(this.btnExportIncidents_Click);
            // 
            // lblIncidentsTotal
            // 
            this.lblIncidentsTotal.AutoSize = true;
            this.lblIncidentsTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIncidentsTotal.Location = new System.Drawing.Point(470, 22);
            this.lblIncidentsTotal.Name = "lblIncidentsTotal";
            this.lblIncidentsTotal.Size = new System.Drawing.Size(145, 15);
            this.lblIncidentsTotal.TabIndex = 9;
            this.lblIncidentsTotal.Text = "Всего инцидентов: 0";
            // 
            // btnIncidentsRefresh
            // 
            this.btnIncidentsRefresh.Location = new System.Drawing.Point(374, 17);
            this.btnIncidentsRefresh.Name = "btnIncidentsRefresh";
            this.btnIncidentsRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnIncidentsRefresh.TabIndex = 8;
            this.btnIncidentsRefresh.Text = "Обновить";
            this.btnIncidentsRefresh.UseVisualStyleBackColor = true;
            this.btnIncidentsRefresh.Click += new System.EventHandler(this.btnIncidentsRefresh_Click);
            // 
            // dtpIncidentsTo
            // 
            this.dtpIncidentsTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIncidentsTo.Location = new System.Drawing.Point(265, 20);
            this.dtpIncidentsTo.Name = "dtpIncidentsTo";
            this.dtpIncidentsTo.Size = new System.Drawing.Size(100, 20);
            this.dtpIncidentsTo.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "по";
            // 
            // dtpIncidentsFrom
            // 
            this.dtpIncidentsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIncidentsFrom.Location = new System.Drawing.Point(134, 20);
            this.dtpIncidentsFrom.Name = "dtpIncidentsFrom";
            this.dtpIncidentsFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpIncidentsFrom.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Период инцидентов: с";
            // 
            // dgvIncidents
            // 
            this.dgvIncidents.AllowUserToAddRows = false;
            this.dgvIncidents.AllowUserToDeleteRows = false;
            this.dgvIncidents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIncidents.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvIncidents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncidents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncidents.Location = new System.Drawing.Point(0, 0);
            this.dgvIncidents.Name = "dgvIncidents";
            this.dgvIncidents.ReadOnly = true;
            this.dgvIncidents.Size = new System.Drawing.Size(1076, 525);
            this.dgvIncidents.TabIndex = 1;
            // 
            // panelEmployees
            // 
            this.panelEmployees.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelEmployees.Controls.Add(this.btnExportEmployees);
            this.panelEmployees.Controls.Add(this.btnEmployeesRefresh);
            this.panelEmployees.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEmployees.Location = new System.Drawing.Point(0, 0);
            this.panelEmployees.Name = "panelEmployees";
            this.panelEmployees.Size = new System.Drawing.Size(1076, 60);
            this.panelEmployees.TabIndex = 2;
            // 
            // btnExportEmployees
            // 
            this.btnExportEmployees.Location = new System.Drawing.Point(106, 17);
            this.btnExportEmployees.Name = "btnExportEmployees";
            this.btnExportEmployees.Size = new System.Drawing.Size(90, 26);
            this.btnExportEmployees.TabIndex = 9;
            this.btnExportEmployees.Text = "Экспорт";
            this.btnExportEmployees.UseVisualStyleBackColor = true;
            // 
            // btnEmployeesRefresh
            // 
            this.btnEmployeesRefresh.Location = new System.Drawing.Point(10, 17);
            this.btnEmployeesRefresh.Name = "btnEmployeesRefresh";
            this.btnEmployeesRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnEmployeesRefresh.TabIndex = 8;
            this.btnEmployeesRefresh.Text = "Обновить";
            this.btnEmployeesRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.Location = new System.Drawing.Point(0, 60);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.Size = new System.Drawing.Size(1076, 465);
            this.dgvEmployees.TabIndex = 0;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.SteelBlue;
            this.panelSummary.Controls.Add(this.lblMonthlyIncidents);
            this.panelSummary.Controls.Add(this.lblEmployeesCount);
            this.panelSummary.Controls.Add(this.lblActiveContracts);
            this.panelSummary.Controls.Add(this.lblMonthlyRevenue);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.ForeColor = System.Drawing.Color.White;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1084, 80);
            this.panelSummary.TabIndex = 1;
            // 
            // lblMonthlyIncidents
            // 
            this.lblMonthlyIncidents.AutoSize = true;
            this.lblMonthlyIncidents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMonthlyIncidents.Location = new System.Drawing.Point(700, 30);
            this.lblMonthlyIncidents.Name = "lblMonthlyIncidents";
            this.lblMonthlyIncidents.Size = new System.Drawing.Size(189, 17);
            this.lblMonthlyIncidents.TabIndex = 3;
            this.lblMonthlyIncidents.Text = "Инцидентов за месяц: 0";
            // 
            // lblEmployeesCount
            // 
            this.lblEmployeesCount.AutoSize = true;
            this.lblEmployeesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEmployeesCount.Location = new System.Drawing.Point(700, 10);
            this.lblEmployeesCount.Name = "lblEmployeesCount";
            this.lblEmployeesCount.Size = new System.Drawing.Size(219, 17);
            this.lblEmployeesCount.TabIndex = 2;
            this.lblEmployeesCount.Text = "Работающих сотрудников: 0";
            // 
            // lblActiveContracts
            // 
            this.lblActiveContracts.AutoSize = true;
            this.lblActiveContracts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblActiveContracts.Location = new System.Drawing.Point(20, 30);
            this.lblActiveContracts.Name = "lblActiveContracts";
            this.lblActiveContracts.Size = new System.Drawing.Size(178, 17);
            this.lblActiveContracts.TabIndex = 1;
            this.lblActiveContracts.Text = "Активных договоров: 0";
            // 
            // lblMonthlyRevenue
            // 
            this.lblMonthlyRevenue.AutoSize = true;
            this.lblMonthlyRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMonthlyRevenue.Location = new System.Drawing.Point(20, 10);
            this.lblMonthlyRevenue.Name = "lblMonthlyRevenue";
            this.lblMonthlyRevenue.Size = new System.Drawing.Size(163, 17);
            this.lblMonthlyRevenue.TabIndex = 0;
            this.lblMonthlyRevenue.Text = "Выручка за месяц: 0";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.LightGray;
            this.panelBottom.Controls.Add(this.btnRefreshAll);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 631);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1084, 50);
            this.panelBottom.TabIndex = 2;
            // 
            // btnRefreshAll
            // 
            this.btnRefreshAll.Location = new System.Drawing.Point(20, 12);
            this.btnRefreshAll.Name = "btnRefreshAll";
            this.btnRefreshAll.Size = new System.Drawing.Size(120, 26);
            this.btnRefreshAll.TabIndex = 1;
            this.btnRefreshAll.Text = "Обновить все";
            this.btnRefreshAll.UseVisualStyleBackColor = true;
            this.btnRefreshAll.Click += new System.EventHandler(this.btnRefreshAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(984, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 26);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelBottom);
            this.MinimumSize = new System.Drawing.Size(1100, 720);
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Отчеты - АИС Охранного Предприятия";
            this.tabControl1.ResumeLayout(false);
            this.tabRevenue.ResumeLayout(false);
            this.panelRevenueFilter.ResumeLayout(false);
            this.panelRevenueFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).EndInit();
            this.tabContracts.ResumeLayout(false);
            this.panelContractsFilter.ResumeLayout(false);
            this.panelContractsFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContracts)).EndInit();
            this.tabIncidents.ResumeLayout(false);
            this.panelIncidentsFilter.ResumeLayout(false);
            this.panelIncidentsFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidents)).EndInit();
            this.panelEmployees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}