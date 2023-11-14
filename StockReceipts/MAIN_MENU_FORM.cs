using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockReceipts
{

    public partial class MAINMENUform : Form
    {
        private List<Button> allButtons;
        public MAINMENUform()
        {
            InitializeComponent();
            MAINMENU_BUTTON_salesregister.Focus();
            InitializeLabels();
            allButtons = new List<Button>
    {
        MAINMENU_button_userconfig,
        MAINMENU_BUTTON_salesregister,
        MAINMENU_BUTTON_salesLOOKUP,
        MAINMENU_button_salesconfig

    };
            UpdateButtonPermissions();
            AttachGlobalTextBoxEvents();
            WireUpButtonEvents();
            

        }

        public void ResetButtonHighlights()
        {
            if (allButtons == null) return;

            foreach (var button in allButtons)
            {
                if (button != null)
                {
                    button.BackColor = SystemColors.Control; // Reset to default color
                }
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null)
            {
                txtBox.BackColor = Color.LightYellow;  // or any color you prefer
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null)
            {
                txtBox.BackColor = Color.White;  // reset to the default color
            }
        }



        private void AttachGlobalTextBoxEvents()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Enter += TextBox_Enter;
                    c.Leave += TextBox_Leave;
                }
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
                    AttachGlobalTextBoxEvents(c);  // Recursively apply to child controls
                }
            }
        }


        private void MAINMENU_KeyDown(object sender, KeyEventArgs e)
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

        private void WireUpButtonEvents()
        {
            MAINMENU_button_userconfig.Click += button_Click;
            MAINMENU_button_salesconfig.Click += button_Click;
            MAINMENU_BUTTON_salesregister.Click += button_Click;
            MAINMENU_BUTTON_salesLOOKUP.Click += button_Click;

        }

        private void button_Click(object sender, EventArgs e)
        {
            ResetButtonHighlights();

            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Highlight the clicked button
                clickedButton.BackColor = Color.LightYellow; // or any highlight color
            }
            UserControl toLoad = null;

            switch (clickedButton.Name)
            {
                case "MAINMENU_button_userconfig":
                    toLoad = new UserConfigControl();
                    break;

                case "MAINMENU_BUTTON_salesregister":
                    toLoad = new SalesRegisterControl(Program.AppConfigInstance.PriceUpdateMode, Program.AppConfigInstance.SalesPriceUpdateMode, FormMode.PurchaseMode);
                    break;

                case "MAINMENU_BUTTON_salesLOOKUP":
                    toLoad = new SalesRegisterControl(Program.AppConfigInstance.PriceUpdateMode, Program.AppConfigInstance.SalesPriceUpdateMode, FormMode.SearchAndModifyMode);
                    break;
                case "MAINMENU_button_salesconfig":
                    if (!IsFormAlreadyOpen(typeof(Config_Form)))
                    {
                        contentPanel.Controls.Clear();
                        Config_Form configForm = new Config_Form();
                        configForm.ConfigurationChanged += ConfigForm_ConfigurationChanged;
                        configForm.Show();

                    }
                    break;

                default:
                    // Handle unknown button or do nothing
                    break;
            }

            if (toLoad != null)
                LoadUserControl(toLoad);
        }
        public void ClearAndReloadContentPanel()
        {
            contentPanel.Controls.Clear();
            // Optionally, reload or reset content panel here if needed
        }
        private void ConfigForm_ConfigurationChanged()
        {
            ResetButtonHighlights();
        }
        private bool IsFormAlreadyOpen(Type formType)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadUserControl(UserControl control)
        {
            contentPanel.Controls.Clear();  // Assuming you have a Panel named contentPanel in your main form.
            control.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(control);
        }

        public static List<bool> GetPermissionsList()
        {
            if (string.IsNullOrEmpty(Program.LoggedInPermissions))
                return new List<bool>();

            return Program.LoggedInPermissions.Split(',').Select(p => p.Trim() == "1").ToList();
        }

        public void UpdateButtonPermissions()
        {
            MAINMENU_button_userconfig.Enabled = Program.HasPermission("MAINMENU_button_userconfig");
            MAINMENU_BUTTON_salesregister.Enabled = Program.HasPermission("MAINMENU_BUTTON_salesregister");
            MAINMENU_button_salesconfig.Enabled = Program.HasPermission("MAINMENU_button_salesconfig");
            MAINMENU_BUTTON_salesLOOKUP.Enabled = Program.HasPermission("MAINMENU_salescancel") || Program.HasPermission("MAINMENU_salesreprint");
        }



        private void InitializeLabels()
        {
            MAINMENU_USERNAMES_lbl.Text = $"{Program.LoggedInName} {Program.LoggedInLastName1} {Program.LoggedInLastName2}";
            MAINMENU_USERNAME_LBL.Text = Program.LoggedInUsername;
            MAINMENU_ACTUALDATE_LBL.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MAINMENU_BRANCHNUMBER_LBL.Text = Program.LoggedInBranchNumber;
            MAINMENU_BUTTON_salesregister.Focus();
        }



        private void CloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
