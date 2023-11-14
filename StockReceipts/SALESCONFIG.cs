using System.Windows.Forms;
using System;
using System.IO;

namespace StockReceipts
{

    public partial class Config_Form : Form
    {
        public Config_Form()
        {
            InitializeComponent();  // Move this to the constructor
            this.FormClosing += Config_Form_FormClosing; // Subscribe to the FormClosing event
        }
        private void Config_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Raise the event whenever the form is closing
            ConfigurationChanged?.Invoke();
        }

        public delegate void ConfigurationChangedHandler();
        public event ConfigurationChangedHandler ConfigurationChanged;
        private void accept_Click(object sender, EventArgs e)
        {
            // For Price Configuration
            if (Config_Form_price_1.Checked)
                Program.AppConfigInstance.PriceUpdateMode = 1;
            else if (Config_Form_price_2.Checked)
                Program.AppConfigInstance.PriceUpdateMode = 2;
            else if (Config_Form_price_3.Checked)
                Program.AppConfigInstance.PriceUpdateMode = 3;

            // For Sales Price Configuration
            if (Config_Form_salesprice_1.Checked)
                Program.AppConfigInstance.SalesPriceUpdateMode = 1;
            else if (Config_Form_salesprice_2.Checked)
                Program.AppConfigInstance.SalesPriceUpdateMode = 2;

            // Save configuration
            string configFilePath = Path.Combine(Application.StartupPath, "appConfig.bin");
            Program.AppConfigInstance.SaveConfiguration(Program.AppConfigInstance, configFilePath);

            this.Close();
        }

        private void Config_Form_Load(object sender, EventArgs e)
        {
            // Set the default or previously selected values on form load

            // For Price Configuration
            switch (Program.AppConfigInstance.PriceUpdateMode)
            {
                case 1:
                    Config_Form_price_1.Checked = true;
                    break;
                case 2:
                    Config_Form_price_2.Checked = true;
                    break;
                case 3:
                    Config_Form_price_3.Checked = true;
                    break;
                default:
                    Config_Form_price_1.Checked = true; // Default value
                    break;
            }

            // For Sales Price Configuration
            switch (Program.AppConfigInstance.SalesPriceUpdateMode)
            {
                case 1:
                    Config_Form_salesprice_1.Checked = true;
                    break;
                case 2:
                    Config_Form_salesprice_2.Checked = true;
                    break;
                default:
                    Config_Form_salesprice_1.Checked = true; // Default value
                    break;
            }
        }
    }
}
