using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockReceipts
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            this.Shown += (s, e) => justShown = true;
        }


        private bool justShown = true;
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter && justShown)
            {
                justShown = false;
                return true;  // This will consume the Enter key press
            }
            return base.ProcessDialogKey(keyData);
        }

        private void PasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Password.Text == "ms123")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
    }
}
