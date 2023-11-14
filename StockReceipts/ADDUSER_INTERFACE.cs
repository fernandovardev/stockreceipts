using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StockReceipts
{
    public partial class UserConfigControl : UserControl
    {
        public UserConfigControl()
        {
            InitializeComponent();
            ADDUSER_ID.Text=GetNextUserId(Program.ConstructTableName("Users"));
            ADDUSER_LASTNAME1.Leave += ADDUSER_LASTNAME1_Leave;
            ADDUSER_LASTNAME2.Leave += ADDUSER_LASTNAME2_Leave;
            ADDUSER_NAME.Leave += ADDUSER_NAME_Leave;
            this.ADDUSER_USERNAME.KeyDown += new KeyEventHandler(this.ADDUSER_USERNAME_KeyDown);
        }
        private void ADDUSER_USERNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                PopulateComboBoxItems(); // Ensure the ComboBox has the latest data
                ToggleUserInput(true); // Switch to ComboBox
                e.Handled = true;
            }
        }

        private void ToggleUserInput(bool showComboBox)
        {
            ADDUSER_USERNAME.Visible = !showComboBox;
            userComboBox.Visible = showComboBox;

            if (showComboBox)
            {
                userComboBox.Text = ADDUSER_USERNAME.Text;
                userComboBox.Focus();
            }
            else
            {
                ADDUSER_USERNAME.Text = userComboBox.Text;
                ADDUSER_USERNAME.Focus();
            }
        }

        private void PopulateComboBoxItems()
        {
            userComboBox.Items.Clear(); // Clear existing items before loading new ones

            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [User] FROM " + Program.ConstructTableName("Users");

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            AutoCompleteStringCollection userCollection = new AutoCompleteStringCollection();

                            while (reader.Read())
                            {
                                string userName = reader["User"].ToString();
                                userComboBox.Items.Add(userName); // Populate ComboBox
                                userCollection.Add(userName);     // Add to auto-complete collection
                            }

                            userComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            userComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            userComboBox.AutoCompleteCustomSource = userCollection;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar nombres de usuario: " + ex.Message);
                }
            }
        }



        public String GetNextUserId(string tableName)
        {
            int highestUserId = 0;

            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                string query = $"SELECT MAX(UserID) FROM {tableName}";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        highestUserId = Convert.ToInt32(result)+1;
                    }
                }
            }

            return highestUserId.ToString().PadLeft(5, '0');
        }
        private bool PasswordsMatch()
        {
            return ADDUSER_PASS.Text == ADDUSER_CONFIRMPASS.Text;
        }

        private void REGISTER_BUTTON_Click(object sender, EventArgs e)
        {
            if (!PasswordsMatch())
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            string userName = ADDUSER_USERNAME.Text;
            string password = ADDUSER_PASS.Text;
            string lastName1 = ADDUSER_LASTNAME1.Text;
            string lastName2 = ADDUSER_LASTNAME2.Text;
            string name = ADDUSER_NAME.Text;
            string status = "A";
            string permissions = GetPermissionsFromChecklists();
            DateTime registerDate = DateTime.Today;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) ||
               string.IsNullOrWhiteSpace(lastName1) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Por favor rellene los campos requeridos.");
                return;
            }

            if (!AtLeastOnePermissionChecked())
            {
                MessageBox.Show("Por favor asigne por lo menos un permiso.");
                return;
            }

           

            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(1) FROM " + Program.ConstructTableName("Users") + " WHERE User = @UserName";
                using (OleDbCommand cmdCheck = new OleDbCommand(checkQuery, connection))
                {
                    cmdCheck.Parameters.AddWithValue("@UserName", userName);

                    int userExists = (int)cmdCheck.ExecuteScalar();
                    if (userExists > 0)
                    {
                        MessageBox.Show("El nombre de usuario ya está registrado.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO " + Program.ConstructTableName("Users") +
                    " ([User], [Pass], [LastName1], [LastName2], [Name], [STATUS], [Permissions], [RegisterDate]) VALUES " +
                    "(@UserName, @Password, @LastName1, @LastName2, @Name, @Status, @Permissions, @RegisterDate)";
                
                using (OleDbCommand cmdInsert = new OleDbCommand(insertQuery, connection))
                {
                    cmdInsert.Parameters.AddWithValue("@UserName", userName);
                    cmdInsert.Parameters.AddWithValue("@Password", password);
                    cmdInsert.Parameters.AddWithValue("@LastName1", lastName1);
                    cmdInsert.Parameters.AddWithValue("@LastName2", lastName2);
                    cmdInsert.Parameters.AddWithValue("@Name", name);
                    cmdInsert.Parameters.AddWithValue("@Status", status);
                    cmdInsert.Parameters.AddWithValue("@Permissions", permissions);
                    cmdInsert.Parameters.Add("@RegisterDate", OleDbType.Date).Value = registerDate;

                    int rowsInserted = cmdInsert.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        MessageBox.Show("¡Usuario registrado con éxito!");
                        ResetFields();
                    }
                }
            }
        }

        private string GetPermissionsFromChecklists()
        {
            StringBuilder permissions = new StringBuilder();

            foreach (var item in CONFIG_CHECKLISTBOX.Items)
            {
                permissions.Append(CONFIG_CHECKLISTBOX.CheckedItems.Contains(item) ? "1" : "0").Append(",");
            }

            foreach (var item in SALES_CHECKLISTBOX.Items)
            {
                permissions.Append(SALES_CHECKLISTBOX.CheckedItems.Contains(item) ? "1" : "0").Append(",");
            }

            return permissions.ToString().TrimEnd(',');
        }

        private void PopulateChecklistsFromPermissions(string permissions)
        {
            var permissionsArr = permissions.Split(',');

            if (permissionsArr.Length != (CONFIG_CHECKLISTBOX.Items.Count + SALES_CHECKLISTBOX.Items.Count))
            {
                MessageBox.Show("Permisos asignados son inconsistentes con la interfaz.");
                return;
            }


        }
        private int PopulateChecklistFromPermissionsArray(CheckedListBox clb, string[] permissionsArr, int startIndex)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (startIndex + i < permissionsArr.Length)
                {
                    clb.SetItemChecked(i, permissionsArr[startIndex + i] == "1");
                }
                else
                {
                    // The permissions string doesn't have enough entries for this checklistbox
                    MessageBox.Show($"Permisos insuficientes para {clb.Name}. Revisa los permisos.");
                    break;
                }
            }

            return startIndex + clb.Items.Count;
        }


       

        private void UpdateFormFields()
        {
            if (currentUserDetails == null) return;

            ADDUSER_NAME.Text = currentUserDetails.Name;
            ADDUSER_USERNAME.Text = currentUserDetails.Username;
            ADDUSER_LASTNAME1.Text = currentUserDetails.LastName1;
            ADDUSER_LASTNAME2.Text = currentUserDetails.LastName2;
            ADDUSER_PASS.Text = currentUserDetails.Pass;
            ADDUSER_CONFIRMPASS.Text = currentUserDetails.Pass;
            ADDUSER_STATUS.Text = currentUserDetails.Status == "A" ? "ACTIVADO" : "DESACTIVADO";
            PopulateChecklistsFromPermissions(currentUserDetails.Permissions);
        }
       
            
        private void ADDUSER_NAME_Leave(object sender, EventArgs e)
        {
            ADDUSER_NAME.Text = ADDUSER_NAME.Text.ToUpper();

            
        }

        private void ADDUSER_ID_Leave(object sender, EventArgs e)
        {
           
            {
                string ID = ADDUSER_ID.Text;
                if (!string.IsNullOrWhiteSpace(ID))
                {
                    bool userFound = SearchUser(ID);

                    // Check if user was found and update button visibility
                    if (userFound && currentUserDetails != null)
                    {
                        // If user status is 'A', allow only deactivation
                        // If user status is 'D', allow only activation
                        ACTIVATE_BUTTON.Visible = currentUserDetails.Status == "D";
                        DEACTIVATE_BUTTON.Visible = currentUserDetails.Status == "A";
                        SAVECHANGES_BUTTON.Visible = currentUserDetails.Status == "A";
                        ADDUSER_ID.Text= ADDUSER_ID.Text.PadLeft(5,'0');
                    }
                    else
                    {
                        REGISTER_BUTTON.Visible = true;
                        ACTIVATE_BUTTON.Visible = false;
                        DEACTIVATE_BUTTON.Visible = false;
                    }
                }
                else
                {
                    // Hide both buttons if ID is empty
                    ACTIVATE_BUTTON.Visible = false;
                    DEACTIVATE_BUTTON.Visible = false;
                }

                // Always hide register button when leaving the ID field
            }
        }


        private bool SearchUser(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                MessageBox.Show("Por favor ingrese un ID de usuario.");
                return false;
            }

            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                string searchQuery = "SELECT * FROM " +
                       Program.ConstructTableName("Users") + " WHERE UserID = @UserID";
                using (OleDbCommand cmdSearch = new OleDbCommand(searchQuery, connection))
                {
                    cmdSearch.Parameters.AddWithValue("@UserID", userID);

                    using (OleDbDataReader reader = cmdSearch.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentUserDetails = new UserDetails
                            {
                                UserID = reader["UserID"].ToString(),
                                Username = reader["User"].ToString(),
                                Name = reader["Name"].ToString(),
                                LastName1 = reader["LastName1"].ToString(),
                                LastName2 = reader["LastName2"].ToString(),
                                Pass = reader["Pass"].ToString(),
                                Status = reader["STATUS"].ToString(),
                                Permissions = reader["Permissions"].ToString()
                            };
                            REGISTER_BUTTON.Visible = false;
                            UpdateFormFields();  // Populate the form fields from currentUserDetails
                            return true;
                        }
                    }
                }
            }
            ResetFields();
            MessageBox.Show("Usuario no encontrado.");
            return false; // User not found
        }
        private bool SearchUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Por favor ingrese un nombre de usuario.");
                return false;
            }

            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                string searchQuery = "SELECT * FROM " +
                       Program.ConstructTableName("Users") + " WHERE [User] = @Username";
                using (OleDbCommand cmdSearch = new OleDbCommand(searchQuery, connection))
                {
                    cmdSearch.Parameters.AddWithValue("@Username", username);

                    using (OleDbDataReader reader = cmdSearch.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentUserDetails = new UserDetails
                            {
                                UserID = reader["UserID"].ToString().PadLeft(5,'0'),
                                Username = reader["User"].ToString(),
                                Name = reader["Name"].ToString(),
                                LastName1 = reader["LastName1"].ToString(),
                                LastName2 = reader["LastName2"].ToString(),
                                Pass = reader["Pass"].ToString(),
                                Status = reader["STATUS"].ToString(),
                                Permissions = reader["Permissions"].ToString()
                            };
                            REGISTER_BUTTON.Visible = false;
                            UpdateFormFields();
                            ADDUSER_ID.Text= currentUserDetails.UserID;
                            ACTIVATE_BUTTON.Visible = currentUserDetails.Status == "D";
                            DEACTIVATE_BUTTON.Visible = currentUserDetails.Status == "A";
                            SAVECHANGES_BUTTON.Visible = currentUserDetails.Status == "A";
                            return true;
                        }
                    }
                }
                
            }
            ResetFields();
            MessageBox.Show("Usuario no encontrado.");
            return false; // User not found
        }

        private void SAVECHANGES_BUTTON_Click(object sender, EventArgs e)
        {
            if (!PasswordsMatch())
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            if (currentUserDetails == null)
            {
                MessageBox.Show("No se ha cargado ningún usuario para actualizar.");
                return;
            }

            string newUsername = ADDUSER_USERNAME.Text;
            if (newUsername != currentUserDetails.Username && UsernameExists(newUsername))
            {
                MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.");
                return;
            }

            currentUserDetails.Username = newUsername;
            currentUserDetails.Name = ADDUSER_NAME.Text;
            currentUserDetails.LastName1 = ADDUSER_LASTNAME1.Text;
            currentUserDetails.LastName2 = ADDUSER_LASTNAME2.Text;
            currentUserDetails.Pass = ADDUSER_PASS.Text;
            currentUserDetails.Permissions = GetPermissionsFromChecklists();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE " + Program.ConstructTableName("Users") +
                         " SET [User] = @NewUsername, Name = @Name, LastName1 = @LastName1, LastName2 = @LastName2, " +
                         "Pass = @Pass, Permissions = @Permissions WHERE UserID = @UserID";

                    using (OleDbCommand updateCmd = new OleDbCommand(updateQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@NewUsername", currentUserDetails.Username);
                        updateCmd.Parameters.AddWithValue("@Name", currentUserDetails.Name);
                        updateCmd.Parameters.AddWithValue("@LastName1", currentUserDetails.LastName1);
                        updateCmd.Parameters.AddWithValue("@LastName2", currentUserDetails.LastName2);
                        updateCmd.Parameters.AddWithValue("@Pass", currentUserDetails.Pass);
                        updateCmd.Parameters.AddWithValue("@Permissions", currentUserDetails.Permissions);
                        updateCmd.Parameters.AddWithValue("@UserID", currentUserDetails.UserID); // Using UserID

                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Configuración de usuario actualizada con éxito.");
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar actualizar configuración de usuario.");
                        }

                        if (currentUserDetails.Username.Equals(Program.LoggedInUsername, StringComparison.OrdinalIgnoreCase))
                        {
                            // Update logged-in user's permissions
                            Program.LoggedInPermissions = currentUserDetails.Permissions;

                            // Refresh UI elements based on new permissions
                            using (var mainMenuForm = Application.OpenForms["MAINMENUform"] as MAINMENUform)
                            {
                                if (mainMenuForm != null)
                                {
                                    mainMenuForm.UpdateButtonPermissions();
                                    mainMenuForm.ClearAndReloadContentPanel();
                                }
                            }
                        }

                    }
                }
                ResetFields();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error durante la actualización: " + ex.Message);
            }
        }



        private bool UsernameExists(string username)
        {
            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM " + Program.ConstructTableName("Users") +
                               " WHERE [User] = @Username";

                // Exclude the current user's username if it's set
                if (!string.IsNullOrEmpty(currentUserDetails.Username))
                {
                    query += " AND [User] <> @CurrentUsername";
                }

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    // Add the current user's username parameter if applicable
                    if (!string.IsNullOrEmpty(currentUserDetails.Username))
                    {
                        cmd.Parameters.AddWithValue("@CurrentUsername", currentUserDetails.Username);
                    }

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        


        private class UserDetails
        {
            public string UserID { get; set; }
            public string Username { get; set; }
            public string Pass { get; set; }
            public string Name { get; set; }
            public string LastName1 { get; set; }
            public string LastName2 { get; set; }
            public string Status { get; set; }
            public string Permissions { get; set; }
            



        }

        private UserDetails currentUserDetails;

        private void ACTIVATE_BUTTON_Click(object sender, EventArgs e)
        {
            // Check if a user has been loaded
            if (currentUserDetails == null)
            {
                MessageBox.Show("No se ha cargado ningún usuario para activar.");
                return;
            }

            if (!PasswordsMatch())
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            UpdateUserStatus("A");
            MessageBox.Show("Usuario activado con éxito.");
            currentUserDetails.Status = "A";
            ResetFields();

        }


        private void DEACTIVATE_BUTTON_Click(object sender, EventArgs e)
        {
            // Check if a user has been loaded
            if (currentUserDetails == null)
            {
                MessageBox.Show("No se ha cargado ningún usuario para desactivar.");
                return;
            }

            if (!PasswordsMatch())
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            UpdateUserStatus("D");
            MessageBox.Show("Usuario desactivado con éxito.");
            currentUserDetails.Status = "D";
            ResetFields();

        }


        private void UpdateUserStatus(string status)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE " + Program.ConstructTableName("Users") + " SET STATUS = @Status WHERE User = @User";
                    using (OleDbCommand updateCmd = new OleDbCommand(query, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@Status", status); // Note the order
                        updateCmd.Parameters.AddWithValue("@User", ADDUSER_USERNAME.Text);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user status: " + ex.Message);
            }

        }
        public void AttachGlobalTextBoxEvents(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c is TextBox)
                {
                    c.Enter += TextBox_Enter;
                    c.Leave += TextBox_Leave;
                }
                if (c.HasChildren)
                {
                    AttachGlobalTextBoxEvents(c);
                }
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                txtBox.BackColor = Color.LightYellow;  // or any color you prefer
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                txtBox.BackColor = Color.White;  // reset to the default color
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
        private bool AtLeastOnePermissionChecked()
        {
            return CONFIG_CHECKLISTBOX.CheckedItems.Count > 0 || SALES_CHECKLISTBOX.CheckedItems.Count > 0;
        }


        private void ResetFields()
        {
            ADDUSER_USERNAME.Text = string.Empty;
            ADDUSER_PASS.Text = string.Empty;
            ADDUSER_LASTNAME1.Text = string.Empty;
            ADDUSER_LASTNAME2.Text = string.Empty;
            ADDUSER_NAME.Text = string.Empty;
            ADDUSER_STATUS.Text = string.Empty;
            ADDUSER_CONFIRMPASS.Text = string.Empty;

            // Uncheck all items in CONFIG_CHECKLISTBOX
            for (int i = 0; i < CONFIG_CHECKLISTBOX.Items.Count; i++)
            {
                CONFIG_CHECKLISTBOX.SetItemChecked(i, false);
            }

            // Uncheck all items in SALES_CHECKLISTBOX
            for (int i = 0; i < SALES_CHECKLISTBOX.Items.Count; i++)
            {
                SALES_CHECKLISTBOX.SetItemChecked(i, false);
            }
            ADDUSER_ID.Text = GetNextUserId(Program.ConstructTableName("Users"));
            REGISTER_BUTTON.Visible = true;
            DEACTIVATE_BUTTON.Visible = false;
            ACTIVATE_BUTTON.Visible = false;
            SAVECHANGES_BUTTON.Visible = false;

        }




        private void ADDUSER_LASTNAME1_Leave(object sender, EventArgs e)
        {
            ADDUSER_LASTNAME1.Text = ADDUSER_LASTNAME1.Text.ToUpper();
        }

        private void ADDUSER_LASTNAME2_Leave(object sender, EventArgs e)
        {
            ADDUSER_LASTNAME2.Text = ADDUSER_LASTNAME2.Text.ToUpper();
        }

        private void UserConfigControl_Load(object sender, EventArgs e)
        {
            ADDUSER_USERNAME.Focus();
            AttachGlobalTextBoxEvents(this);
            
        }

        private void ADDUSER_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userComboBox.SelectedIndex != -1) // Check if an item is selected
            {
                string selectedUsername = userComboBox.SelectedItem.ToString();
                if (SearchUserByUsername(selectedUsername))
                {
                    // Set the username text box value and switch visibility
                    ADDUSER_USERNAME.Text = selectedUsername;
                    ToggleUserInput(false); // Make ComboBox invisible and TextBox visible
                }
            }
        }

        private void UserComboBox_Leave(object sender, EventArgs e)
        {
            userComboBox.Visible = false;
            ADDUSER_USERNAME.Visible = true;

        }
    }
}
