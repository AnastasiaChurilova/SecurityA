namespace Security2
{
    partial class EmployeeEditForm
    {
        private System.ComponentModel.IContainer components = null;

        // Основные поля
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;

        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.ComboBox cmbEducation;
        private System.Windows.Forms.CheckBox chkLicense;
        private System.Windows.Forms.TextBox txtLicenseNumber;
        private System.Windows.Forms.CheckBox chkHasLicenseDate;
        private System.Windows.Forms.DateTimePicker dtpLicenseDate;
        private System.Windows.Forms.ComboBox cmbAccessLevel;

        // Фото
        private System.Windows.Forms.GroupBox groupBoxPhoto;
        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Button btnLoadPhoto;
        private System.Windows.Forms.Button btnDeletePhoto;

        // Кнопки
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelButtons;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.cmbEducation = new System.Windows.Forms.ComboBox();
            this.chkLicense = new System.Windows.Forms.CheckBox();
            this.txtLicenseNumber = new System.Windows.Forms.TextBox();
            this.chkHasLicenseDate = new System.Windows.Forms.CheckBox();
            this.dtpLicenseDate = new System.Windows.Forms.DateTimePicker();
            this.cmbAccessLevel = new System.Windows.Forms.ComboBox();
            this.groupBoxPhoto = new System.Windows.Forms.GroupBox();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.btnLoadPhoto = new System.Windows.Forms.Button();
            this.btnDeletePhoto = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.groupBoxPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Должность:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Дата приема:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Оклад:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Образование:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Лицензия на оружие:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 270);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Номер лицензии:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 310);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Дата выдачи лиц.:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 350);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Уровень доступа:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(96, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "руб.";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(150, 27);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(300, 20);
            this.txtFullName.TabIndex = 10;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(150, 67);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(200, 20);
            this.txtPosition.TabIndex = 11;
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHireDate.Location = new System.Drawing.Point(150, 107);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(120, 20);
            this.dtpHireDate.TabIndex = 12;
            // 
            // txtSalary
            // 
            this.txtSalary.Location = new System.Drawing.Point(150, 147);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(100, 20);
            this.txtSalary.TabIndex = 13;
            this.txtSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbEducation
            // 
            this.cmbEducation.FormattingEnabled = true;
            this.cmbEducation.Location = new System.Drawing.Point(150, 187);
            this.cmbEducation.Name = "cmbEducation";
            this.cmbEducation.Size = new System.Drawing.Size(200, 21);
            this.cmbEducation.TabIndex = 14;
            // 
            // chkLicense
            // 
            this.chkLicense.AutoSize = true;
            this.chkLicense.Location = new System.Drawing.Point(150, 229);
            this.chkLicense.Name = "chkLicense";
            this.chkLicense.Size = new System.Drawing.Size(15, 14);
            this.chkLicense.TabIndex = 15;
            this.chkLicense.UseVisualStyleBackColor = true;
            this.chkLicense.CheckedChanged += new System.EventHandler(this.chkLicense_CheckedChanged);
            // 
            // txtLicenseNumber
            // 
            this.txtLicenseNumber.Enabled = false;
            this.txtLicenseNumber.Location = new System.Drawing.Point(150, 267);
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new System.Drawing.Size(150, 20);
            this.txtLicenseNumber.TabIndex = 16;
            // 
            // chkHasLicenseDate
            // 
            this.chkHasLicenseDate.AutoSize = true;
            this.chkHasLicenseDate.Enabled = false;
            this.chkHasLicenseDate.Location = new System.Drawing.Point(150, 309);
            this.chkHasLicenseDate.Name = "chkHasLicenseDate";
            this.chkHasLicenseDate.Size = new System.Drawing.Size(15, 14);
            this.chkHasLicenseDate.TabIndex = 17;
            this.chkHasLicenseDate.UseVisualStyleBackColor = true;
            this.chkHasLicenseDate.CheckedChanged += new System.EventHandler(this.chkHasLicenseDate_CheckedChanged);
            // 
            // dtpLicenseDate
            // 
            this.dtpLicenseDate.Enabled = false;
            this.dtpLicenseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLicenseDate.Location = new System.Drawing.Point(170, 307);
            this.dtpLicenseDate.Name = "dtpLicenseDate";
            this.dtpLicenseDate.Size = new System.Drawing.Size(120, 20);
            this.dtpLicenseDate.TabIndex = 18;
            // 
            // cmbAccessLevel
            // 
            this.cmbAccessLevel.FormattingEnabled = true;
            this.cmbAccessLevel.Location = new System.Drawing.Point(150, 347);
            this.cmbAccessLevel.Name = "cmbAccessLevel";
            this.cmbAccessLevel.Size = new System.Drawing.Size(100, 21);
            this.cmbAccessLevel.TabIndex = 19;
            // 
            // groupBoxPhoto
            // 
            this.groupBoxPhoto.Controls.Add(this.pbPhoto);
            this.groupBoxPhoto.Controls.Add(this.btnLoadPhoto);
            this.groupBoxPhoto.Controls.Add(this.btnDeletePhoto);
            this.groupBoxPhoto.Location = new System.Drawing.Point(500, 30);
            this.groupBoxPhoto.Name = "groupBoxPhoto";
            this.groupBoxPhoto.Size = new System.Drawing.Size(300, 350);
            this.groupBoxPhoto.TabIndex = 20;
            this.groupBoxPhoto.TabStop = false;
            this.groupBoxPhoto.Text = "Фотография сотрудника";
            // 
            // pbPhoto
            // 
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Location = new System.Drawing.Point(20, 30);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(260, 240);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 0;
            this.pbPhoto.TabStop = false;
            // 
            // btnLoadPhoto
            // 
            this.btnLoadPhoto.Location = new System.Drawing.Point(20, 290);
            this.btnLoadPhoto.Name = "btnLoadPhoto";
            this.btnLoadPhoto.Size = new System.Drawing.Size(120, 30);
            this.btnLoadPhoto.TabIndex = 1;
            this.btnLoadPhoto.Text = "Загрузить фото";
            this.btnLoadPhoto.UseVisualStyleBackColor = true;
            this.btnLoadPhoto.Click += new System.EventHandler(this.btnLoadPhoto_Click);
            // 
            // btnDeletePhoto
            // 
            this.btnDeletePhoto.Location = new System.Drawing.Point(160, 290);
            this.btnDeletePhoto.Name = "btnDeletePhoto";
            this.btnDeletePhoto.Size = new System.Drawing.Size(120, 30);
            this.btnDeletePhoto.TabIndex = 2;
            this.btnDeletePhoto.Text = "Удалить фото";
            this.btnDeletePhoto.UseVisualStyleBackColor = true;
            this.btnDeletePhoto.Click += new System.EventHandler(this.btnDeletePhoto_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(30, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.LightGray;
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 410);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(820, 50);
            this.panelButtons.TabIndex = 21;
            // 
            // EmployeeEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 460);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxPhoto);
            this.Controls.Add(this.cmbAccessLevel);
            this.Controls.Add(this.dtpLicenseDate);
            this.Controls.Add(this.chkHasLicenseDate);
            this.Controls.Add(this.txtLicenseNumber);
            this.Controls.Add(this.chkLicense);
            this.Controls.Add(this.cmbEducation);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.dtpHireDate);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сотрудник";
            this.groupBoxPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}