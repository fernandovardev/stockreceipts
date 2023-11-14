namespace StockReceipts
{
    partial class Config_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Config_Form_price_1 = new System.Windows.Forms.RadioButton();
            this.Config_Form_price_2 = new System.Windows.Forms.RadioButton();
            this.Config_Form_price_3 = new System.Windows.Forms.RadioButton();
            this.Config_Form_priceconfig_box = new System.Windows.Forms.GroupBox();
            this.Config_Form_salespriceconfig_box = new System.Windows.Forms.GroupBox();
            this.Config_Form_salesprice_1 = new System.Windows.Forms.RadioButton();
            this.Config_Form_salesprice_2 = new System.Windows.Forms.RadioButton();
            this.Accept = new System.Windows.Forms.Button();
            this.Config_Form_priceconfig_box.SuspendLayout();
            this.Config_Form_salespriceconfig_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // Config_Form_price_1
            // 
            this.Config_Form_price_1.AutoSize = true;
            this.Config_Form_price_1.Location = new System.Drawing.Point(18, 19);
            this.Config_Form_price_1.Name = "Config_Form_price_1";
            this.Config_Form_price_1.Size = new System.Drawing.Size(111, 17);
            this.Config_Form_price_1.TabIndex = 0;
            this.Config_Form_price_1.TabStop = true;
            this.Config_Form_price_1.Text = "No cambiar precio";
            this.Config_Form_price_1.UseVisualStyleBackColor = true;
            // 
            // Config_Form_price_2
            // 
            this.Config_Form_price_2.AutoSize = true;
            this.Config_Form_price_2.Location = new System.Drawing.Point(18, 42);
            this.Config_Form_price_2.Name = "Config_Form_price_2";
            this.Config_Form_price_2.Size = new System.Drawing.Size(172, 17);
            this.Config_Form_price_2.TabIndex = 0;
            this.Config_Form_price_2.TabStop = true;
            this.Config_Form_price_2.Text = "Cambiar precio solo si es mayor";
            this.Config_Form_price_2.UseVisualStyleBackColor = true;
            // 
            // Config_Form_price_3
            // 
            this.Config_Form_price_3.AutoSize = true;
            this.Config_Form_price_3.Location = new System.Drawing.Point(18, 65);
            this.Config_Form_price_3.Name = "Config_Form_price_3";
            this.Config_Form_price_3.Size = new System.Drawing.Size(95, 17);
            this.Config_Form_price_3.TabIndex = 0;
            this.Config_Form_price_3.TabStop = true;
            this.Config_Form_price_3.Text = "Cambiar precio";
            this.Config_Form_price_3.UseVisualStyleBackColor = true;
            // 
            // Config_Form_priceconfig_box
            // 
            this.Config_Form_priceconfig_box.Controls.Add(this.Config_Form_price_1);
            this.Config_Form_priceconfig_box.Controls.Add(this.Config_Form_price_3);
            this.Config_Form_priceconfig_box.Controls.Add(this.Config_Form_price_2);
            this.Config_Form_priceconfig_box.Location = new System.Drawing.Point(21, 12);
            this.Config_Form_priceconfig_box.Name = "Config_Form_priceconfig_box";
            this.Config_Form_priceconfig_box.Size = new System.Drawing.Size(200, 100);
            this.Config_Form_priceconfig_box.TabIndex = 1;
            this.Config_Form_priceconfig_box.TabStop = false;
            this.Config_Form_priceconfig_box.Text = "Configuración en precio";
            // 
            // Config_Form_salespriceconfig_box
            // 
            this.Config_Form_salespriceconfig_box.Controls.Add(this.Config_Form_salesprice_1);
            this.Config_Form_salespriceconfig_box.Controls.Add(this.Config_Form_salesprice_2);
            this.Config_Form_salespriceconfig_box.Location = new System.Drawing.Point(21, 118);
            this.Config_Form_salespriceconfig_box.Name = "Config_Form_salespriceconfig_box";
            this.Config_Form_salespriceconfig_box.Size = new System.Drawing.Size(200, 70);
            this.Config_Form_salespriceconfig_box.TabIndex = 1;
            this.Config_Form_salespriceconfig_box.TabStop = false;
            this.Config_Form_salespriceconfig_box.Text = "Configuración en precio de venta";
            // 
            // Config_Form_salesprice_1
            // 
            this.Config_Form_salesprice_1.AutoSize = true;
            this.Config_Form_salesprice_1.Location = new System.Drawing.Point(18, 19);
            this.Config_Form_salesprice_1.Name = "Config_Form_salesprice_1";
            this.Config_Form_salesprice_1.Size = new System.Drawing.Size(156, 17);
            this.Config_Form_salesprice_1.TabIndex = 0;
            this.Config_Form_salesprice_1.TabStop = true;
            this.Config_Form_salesprice_1.Text = "No cambiar precio de venta";
            this.Config_Form_salesprice_1.UseVisualStyleBackColor = true;
            // 
            // Config_Form_salesprice_2
            // 
            this.Config_Form_salesprice_2.AutoSize = true;
            this.Config_Form_salesprice_2.Location = new System.Drawing.Point(18, 42);
            this.Config_Form_salesprice_2.Name = "Config_Form_salesprice_2";
            this.Config_Form_salesprice_2.Size = new System.Drawing.Size(140, 17);
            this.Config_Form_salesprice_2.TabIndex = 0;
            this.Config_Form_salesprice_2.TabStop = true;
            this.Config_Form_salesprice_2.Text = "Cambiar precio de venta";
            this.Config_Form_salesprice_2.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(91, 194);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 2;
            this.Accept.Text = "Aceptar";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // Config_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 248);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.Config_Form_salespriceconfig_box);
            this.Controls.Add(this.Config_Form_priceconfig_box);
            this.Name = "Config_Form";
            this.ShowIcon = false;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.Config_Form_Load);
            this.Config_Form_priceconfig_box.ResumeLayout(false);
            this.Config_Form_priceconfig_box.PerformLayout();
            this.Config_Form_salespriceconfig_box.ResumeLayout(false);
            this.Config_Form_salespriceconfig_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton Config_Form_price_1;
        private System.Windows.Forms.RadioButton Config_Form_price_2;
        private System.Windows.Forms.RadioButton Config_Form_price_3;
        private System.Windows.Forms.GroupBox Config_Form_priceconfig_box;
        private System.Windows.Forms.GroupBox Config_Form_salespriceconfig_box;
        private System.Windows.Forms.RadioButton Config_Form_salesprice_1;
        private System.Windows.Forms.RadioButton Config_Form_salesprice_2;
        private System.Windows.Forms.Button Accept;
    }
}