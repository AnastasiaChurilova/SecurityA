using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Security2
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Server=localhost;Database=C:\DATABASES\JOJO.FDB;
                                   User=SYSDBA;Password=masterkey;
                                   Charset=UTF8;Dialect=3;Port=3050;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Автотест подключения при загрузке
        }

        

        private void BtnOpenEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                // Используем using для автоматического освобождения ресурсов
                using (var employeesForm = new EmployeesForm(connectionString))
                {
                    employeesForm.ShowDialog();
                } // Форма автоматически уничтожается здесь
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы сотрудников:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenContracts_Click(object sender, EventArgs e)
        {
            try
            {
                using (var contractsForm = new ContractsForm(connectionString))
                {
                    contractsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы договоров:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void BtnOpenReports_Click(object sender, EventArgs e)
        {
            try
            {
                var reportsForm = new ReportsForm(connectionString);
                reportsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия отчетов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnOpenEmployees_Click(sender, e);
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var clientsForm = new ClientsForm(connectionString))
                {
                    clientsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы клиентов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void договорыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnOpenContracts_Click(sender, e);
        }

        

        private void выручкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnOpenReports_Click(sender, e);
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Инциденты_Click(object sender, EventArgs e)
        {
            try
            {
                // Используем using для автоматического закрытия формы
                using (var incidentsForm = new IncidentsForm(connectionString))
                {
                    incidentsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы инцидентов:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Добавь эту функцию в Form1 для проверки

    }
}