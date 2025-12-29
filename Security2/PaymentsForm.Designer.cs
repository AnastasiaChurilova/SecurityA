namespace Security2
{
    partial class PaymentsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblContractInfo;
        private System.Windows.Forms.Label lblClientInfo;
        private System.Windows.Forms.Label lblContractAmount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAddPayment;
        private System.Windows.Forms.Button btnEditPayment;
        private System.Windows.Forms.Button btnDeletePayment;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblTotalPending;
        private System.Windows.Forms.Label lblBalance;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblContractAmount = new System.Windows.Forms.Label();
            this.lblClientInfo = new System.Windows.Forms.Label();
            this.lblContractInfo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblTotalPending = new System.Windows.Forms.Label();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnDeletePayment = new System.Windows.Forms.Button();
            this.btnEditPayment = new System.Windows.Forms.Button();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelStats.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelTop.Controls.Add(this.lblContractAmount);
            this.panelTop.Controls.Add(this.lblClientInfo);
            this.panelTop.Controls.Add(this.lblContractInfo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1045, 98);
            this.panelTop.TabIndex = 0;
            // 
            // lblContractAmount
            // 
            this.lblContractAmount.AutoSize = true;
            this.lblContractAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblContractAmount.Location = new System.Drawing.Point(27, 62);
            this.lblContractAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractAmount.Name = "lblContractAmount";
            this.lblContractAmount.Size = new System.Drawing.Size(159, 18);
            this.lblContractAmount.TabIndex = 2;
            this.lblContractAmount.Text = "Сумма договора: 0";
            // 
            // lblClientInfo
            // 
            this.lblClientInfo.AutoSize = true;
            this.lblClientInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClientInfo.Location = new System.Drawing.Point(400, 25);
            this.lblClientInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientInfo.Name = "lblClientInfo";
            this.lblClientInfo.Size = new System.Drawing.Size(62, 18);
            this.lblClientInfo.TabIndex = 1;
            this.lblClientInfo.Text = "Клиент:";
            // 
            // lblContractInfo
            // 
            this.lblContractInfo.AutoSize = true;
            this.lblContractInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblContractInfo.Location = new System.Drawing.Point(27, 25);
            this.lblContractInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractInfo.Name = "lblContractInfo";
            this.lblContractInfo.Size = new System.Drawing.Size(169, 20);
            this.lblContractInfo.TabIndex = 0;
            this.lblContractInfo.Text = "Договор №0 от ...";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 172);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1045, 407);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.LightGray;
            this.panelStats.Controls.Add(this.lblBalance);
            this.panelStats.Controls.Add(this.lblTotalPending);
            this.panelStats.Controls.Add(this.lblTotalPaid);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 98);
            this.panelStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1045, 74);
            this.panelStats.TabIndex = 2;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBalance.Location = new System.Drawing.Point(667, 25);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(74, 18);
            this.lblBalance.TabIndex = 2;
            this.lblBalance.Text = "Итого: 0";
            // 
            // lblTotalPending
            // 
            this.lblTotalPending.AutoSize = true;
            this.lblTotalPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalPending.Location = new System.Drawing.Point(400, 25);
            this.lblTotalPending.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPending.Name = "lblTotalPending";
            this.lblTotalPending.Size = new System.Drawing.Size(87, 18);
            this.lblTotalPending.TabIndex = 1;
            this.lblTotalPending.Text = "Ожидает: 0";
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.AutoSize = true;
            this.lblTotalPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalPaid.Location = new System.Drawing.Point(27, 25);
            this.lblTotalPaid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(94, 18);
            this.lblTotalPaid.TabIndex = 0;
            this.lblTotalPaid.Text = "Оплачено: 0";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.LightGray;
            this.panelBottom.Controls.Add(this.btnDeletePayment);
            this.panelBottom.Controls.Add(this.btnEditPayment);
            this.panelBottom.Controls.Add(this.btnAddPayment);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 579);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1045, 62);
            this.panelBottom.TabIndex = 3;
            // 
            // btnDeletePayment
            // 
            this.btnDeletePayment.Location = new System.Drawing.Point(280, 12);
            this.btnDeletePayment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeletePayment.Name = "btnDeletePayment";
            this.btnDeletePayment.Size = new System.Drawing.Size(133, 37);
            this.btnDeletePayment.TabIndex = 4;
            this.btnDeletePayment.Text = "Удалить";
            this.btnDeletePayment.UseVisualStyleBackColor = true;
            this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);
            // 
            // btnEditPayment
            // 
            this.btnEditPayment.Location = new System.Drawing.Point(147, 12);
            this.btnEditPayment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditPayment.Name = "btnEditPayment";
            this.btnEditPayment.Size = new System.Drawing.Size(133, 37);
            this.btnEditPayment.TabIndex = 3;
            this.btnEditPayment.Text = "Редактировать";
            this.btnEditPayment.UseVisualStyleBackColor = true;
            this.btnEditPayment.Click += new System.EventHandler(this.btnEditPayment_Click);
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(13, 12);
            this.btnAddPayment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(133, 37);
            this.btnAddPayment.TabIndex = 2;
            this.btnAddPayment.Text = "Добавить";
            this.btnAddPayment.UseVisualStyleBackColor = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(760, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(133, 37);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(893, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 37);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PaymentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 641);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1061, 678);
            this.Name = "PaymentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Платежи по договору";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}