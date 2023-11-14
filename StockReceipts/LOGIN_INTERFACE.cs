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
using System.Data.OleDb;
using static System.Windows.Forms.AxHost;
using static StockReceipts.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace StockReceipts
{
    public partial class LoginForm : Form
    {
        public string connectionString = Program.DatabaseConnectionString;
        public LoginForm()
        {
            InitializeComponent();

            if (File.Exists("config.dat"))
            {
                try
                {
                    Program.DatabaseConnectionString = Program.LoadEncryptedConnectionString();
                    Program.LoggedInBranchNumber = GetBranchNumber();

                    if (Program.TestDatabaseConnection(Program.DatabaseConnectionString))
                    {
                        using (OleDbConnection conn = new OleDbConnection(Program.DatabaseConnectionString))
                        {
                            conn.Open();
                            SimpleLogger.Log("Database connection established.");
                            Program.StoreBranchNumberAndTaxRate();

                            string branchNumber = Program.LoggedInBranchNumber;
                            string userTableName = $"{branchNumber}_Users";
                            string registerTableName = $"{branchNumber}_Register";

                            if (!TableExists(conn, userTableName))
                            {
                                CreateUsersTable(conn, userTableName);
                                SimpleLogger.Log($"Created table: {userTableName}");
                            }
                            else
                            {
                                EnsureAdminUserExists(conn, userTableName);
                            }

                            if (!TableExists(conn, registerTableName))
                            {
                                CreateRegisterTable(conn, registerTableName);
                                SimpleLogger.Log($"Created table: {registerTableName}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Falló la conexión a la base de datos. Por favor, reconfigure la ruta de acceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HandleDatabaseAccess();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SimpleLogger.Log($"LoginForm error: {ex.Message}");
                    SimpleLogger.Log(ex.ToString());  // Log the full exception
                    HandleDatabaseAccess();
                }

            }
            else
            {
                MessageBox.Show("Por favor configure la ruta de acceso a la base de datos.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Delay(500).Wait();
                HandleDatabaseAccess();
            }
        }

        private void EnsureAdminUserExists(OleDbConnection conn, string tableName)
        {
            if (!AdminUserExists(conn, tableName))
            {
                InsertAdminUser(conn, tableName);
                SimpleLogger.Log($"Admin user inserted in table: {tableName}");
            }
            else
            {
                SimpleLogger.Log($"Admin user already exists in table: {tableName}");
            }
        }

        // Assuming you already have these methods:

        private void CreateRegisterTable(OleDbConnection conn, string tableName)
        {
            string createTableSQL = $@"CREATE TABLE [{tableName}] (
      [ID] AUTOINCREMENT PRIMARY KEY,
      [Folio] TEXT, 
      [ActionType] TEXT, 
      [Timestamp] DATE, 
      [Comments] TEXT, 
      [PerformedBy] TEXT)";

            try
            {
                using (var createTableCmd = new OleDbCommand(createTableSQL, conn))
                {
                    createTableCmd.ExecuteNonQuery();
                    SimpleLogger.Log($"Created table '{tableName}'.");
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
            }
        }
        private void CloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void HandleDatabaseAccess()
        {
            if (PromptPassword())
            {
                PromptDBForm();

                if (string.IsNullOrEmpty(Program.DatabaseConnectionString))
                {
                    MessageBox.Show("Error al conectar con la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void DB_access_Click(object sender, EventArgs e)
        {
            HandleDatabaseAccess();
        }


        private bool PromptPassword()
        {
            using (PasswordForm passwordForm = new PasswordForm())
            {
                return passwordForm.ShowDialog() == DialogResult.OK;
            }
        }

        private string PromptDBForm()
        {
            using (DBForm dbForm = new DBForm())
            {
                if (dbForm.ShowDialog() == DialogResult.OK)
                    return Program.DatabaseConnectionString;
            }
            return null;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Loginform_txtbox_user.Text.Trim();
            string password = LoginForm_txtbox_pass.Text.Trim();
            string logMessage = $"User {username} attempted to log in.";
            SimpleLogger.Log(logMessage);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            AuthenticationResult authResult = AuthenticateUser(Program.LoggedInBranchNumber, username, password);

            switch (authResult)
            {
                case AuthenticationResult.Success:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    string logMessage2 = $"User {username} received authentication result: {authResult}";
                    SimpleLogger.Log(logMessage2);
                    break;

                case AuthenticationResult.IncorrectPassword:
                    MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case AuthenticationResult.UserDoesNotExist:
                    MessageBox.Show("El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case AuthenticationResult.Error:
                    MessageBox.Show("Error al intentar iniciar sesión. Por favor intente de nuevo más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string logMessage3 = $"Error during user authentication. Exception";
                    SimpleLogger.Log(logMessage3);

                    break;

                default:
                    MessageBox.Show("Error desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }


        public enum AuthenticationResult
        {
            UserDoesNotExist,
            IncorrectPassword,
            Success,
            Error 
        }

        public AuthenticationResult AuthenticateUser(string branchNumber, string username, string password)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(Program.LoadEncryptedConnectionString()))
                {
                    conn.Open();

                    string tableName = $"{branchNumber}_Users";

                    // Check if user exists
                    string userCheckCmdText = $"SELECT COUNT(*) FROM {tableName} WHERE [User] = ?";
                    SimpleLogger.Log($"Executing SQL query: {userCheckCmdText}");

                    using (var userCheckCmd = new System.Data.OleDb.OleDbCommand(userCheckCmdText, conn))
                    {
                        SimpleLogger.Log($"Executing SQL query: {userCheckCmd}");

                        userCheckCmd.Parameters.AddWithValue("@User", username);

                        int userExists = (int)userCheckCmd.ExecuteScalar();
                        if (userExists == 0)
                        {
                            return AuthenticationResult.UserDoesNotExist;
                        }
                    }

                    // User exists, now check password
                    string authCmdText = $"SELECT UserID, Permissions, Name, LastName1, LastName2 FROM {tableName} WHERE [User] = ? AND [Pass] = ?";
                    using (var authCmd = new System.Data.OleDb.OleDbCommand(authCmdText, conn))
                    {
                        authCmd.Parameters.AddWithValue("@User", username);
                        authCmd.Parameters.AddWithValue("@Pass", password);

                        using (var reader = authCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                              
                                Program.LoggedInPermissions = reader["Permissions"].ToString();
                                Program.LoggedInName = reader["Name"].ToString();
                                Program.LoggedInLastName1 = reader["LastName1"].ToString();
                                Program.LoggedInLastName2 = reader["LastName2"].ToString();
                                Program.LoggedInBranchNumber = branchNumber;
                                Program.LoggedInUsername = username;

                                return AuthenticationResult.Success;
                            }
                            else
                            {
                                return AuthenticationResult.IncorrectPassword;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return AuthenticationResult.Error;
            }

        }


        private bool TableExists(OleDbConnection conn, string tableName)
        {
            DataTable dt = conn.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                if (row["TABLE_NAME"].ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    SimpleLogger.Log($"Table found: {tableName}");
                    return true;
                }
            }

            SimpleLogger.Log($"Table not found: {tableName}");
            return false;
        }



        private bool AdminUserExists(OleDbConnection conn, string tableName)
        {
            string checkAdminSQL = $@"SELECT COUNT(*) FROM [{tableName}] WHERE [User] = 'admin'";
            using (OleDbCommand cmd = new OleDbCommand(checkAdminSQL, conn))
            {
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0;
            }
        }

   
        private void CreateUsersTable(OleDbConnection conn, string tableName)
        {
            string createTableSQL = $@"CREATE TABLE [{tableName}] (
        [UserID] AUTOINCREMENT PRIMARY KEY,
        [User] TEXT, 
        [Pass] TEXT, 
        [LastName1] TEXT, 
        [LastName2] TEXT, 
        [STATUS] TEXT, 
        [Name] TEXT, 
        [Permissions] TEXT, 
        [RegisterDate] DATE)";

            try
            {
                using (var createTableCmd = new OleDbCommand(createTableSQL, conn))
                {
                    createTableCmd.ExecuteNonQuery();
                    SimpleLogger.Log($"Created table '{tableName}'.");
                }

                InsertAdminUser(conn, tableName);
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
            }
        }

        private void InsertAdminUser(OleDbConnection conn, string tableName)
        {
            string insertDefaultUserSQL = $@"INSERT INTO [{tableName}] 
        ([User], [Pass], [LastName1], [LastName2], [STATUS], [Name], [Permissions], [RegisterDate]) 
        VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

            try
            {
                using (var cmdInsert = new OleDbCommand(insertDefaultUserSQL, conn))
                {
                    cmdInsert.Parameters.AddWithValue("?", "admin");
                    cmdInsert.Parameters.AddWithValue("?", "1234");
                    cmdInsert.Parameters.AddWithValue("?", "LN1");
                    cmdInsert.Parameters.AddWithValue("?", "LN2");
                    cmdInsert.Parameters.AddWithValue("?", "A");
                    cmdInsert.Parameters.AddWithValue("?", "Admin");
                    cmdInsert.Parameters.AddWithValue("?", "1,1,1,1,1");
                    cmdInsert.Parameters.AddWithValue("?", DateTime.Today);

                    cmdInsert.ExecuteNonQuery();
                    SimpleLogger.Log($"Inserted default admin user into '{tableName}'.");
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.LogException(ex);
            }
        }






        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SendKeys.Send("+{TAB}");
                e.Handled = true;
            }
        }



    }
}
