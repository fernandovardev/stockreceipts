namespace StockReceipts
{

    partial class SearchProviderForm
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
            this.components = new System.ComponentModel.Container();
            this.sUPERSDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._027_CatProveedorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SearchProviderForm_SearchTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dGVProvider = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.sUPERSDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._027_CatProveedorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchProviderForm_SearchTxtBox
            // 
            this.SearchProviderForm_SearchTxtBox.Location = new System.Drawing.Point(12, 25);
            this.SearchProviderForm_SearchTxtBox.Name = "SearchProviderForm_SearchTxtBox";
            this.SearchProviderForm_SearchTxtBox.Size = new System.Drawing.Size(336, 20);
            this.SearchProviderForm_SearchTxtBox.TabIndex = 1;
            this.SearchProviderForm_SearchTxtBox.TextChanged += new System.EventHandler(this.SearchProviderForm_SearchTxtBox_TextChanged);
            this.SearchProviderForm_SearchTxtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dGVProvider_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "BUSCAR PROVEEDOR";
            // 
            // dGVProvider
            // 
            this.dGVProvider.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVProvider.Location = new System.Drawing.Point(12, 51);
            this.dGVProvider.Name = "dGVProvider";
            this.dGVProvider.Size = new System.Drawing.Size(334, 389);
            this.dGVProvider.TabIndex = 3;
            // 
            // SearchProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 452);
            this.Controls.Add(this.dGVProvider);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchProviderForm_SearchTxtBox);
            this.Name = "SearchProviderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CATÁLOGO DE PROVEEDORES";
            this.Load += new System.EventHandler(this.SearchProviderForm_Load);
            this.DoubleClick += new System.EventHandler(this.SearchProviderForm_SearchTxtBox_TextChanged);
            this.Leave += new System.EventHandler(this.SearchProviderForm_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.sUPERSDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._027_CatProveedorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource sUPERSDataSetBindingSource;
        private System.Windows.Forms.BindingSource _027_CatProveedorBindingSource;
        private System.Windows.Forms.TextBox SearchProviderForm_SearchTxtBox;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dGVProvider;
    }
}