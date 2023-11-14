using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace StockReceipts
{
    public partial class DBForm : Form
    {
        public DBForm()
        {
            InitializeComponent();
        }

        private void ConnectionStringTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Proceed to get connection string from user
                GetConnectionStringFromUser();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void GetConnectionStringFromUser()
        {
            string mdbFilePath = ConnectionStringTextBox.Text; // Get route from ConnectionStringTextBox
            string dbPassword = ConnectionStringTextBox_pass.Text; // Get password from the new TextBox

            // Use the fetched database password in the connection string
            Program.DatabaseConnectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath};Jet OLEDB:Database Password={dbPassword};";

            if (Program.TestDatabaseConnection(Program.DatabaseConnectionString))
            {
                SaveEncryptedConnectionString(Program.DatabaseConnectionString);
                MessageBox.Show("Conexión realizada y guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Falló la conexión a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveEncryptedConnectionString(string connectionString)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(connectionString), null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes("config.dat", encryptedData);
        }

        private void DBForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("config.dat"))
            {
                using (PasswordForm passwordForm = new PasswordForm())
                {
                    if (passwordForm.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                }
            }

            // At this point, you either have an existing configuration or the user entered the correct password.
            // The user can now input the .mdb file path.
        }


    }
}
