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
    public partial class CancellationForm : Form
    {
        public string CancellationReason { get; private set; }

        public CancellationForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                CancellationReason = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                CancellationReason = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                CancellationReason = radioButton3.Text;
            }
            else if (radioButton4.Checked)
            {
                // Validate custom reason
                if (string.IsNullOrWhiteSpace(commentTextBox.Text))
                {
                    MessageBox.Show("Please enter a cancellation reason.");
                    return;
                }
                CancellationReason = commentTextBox.Text;
            }
            else
            {
                MessageBox.Show("Please select a cancellation reason.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            commentTextBox.Enabled = radioButton4.Checked;
        }

    }

}
