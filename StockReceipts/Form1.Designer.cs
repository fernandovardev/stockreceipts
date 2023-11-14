namespace StockReceipts
{
    partial class SearchProductForm
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
            this.dGVProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchProductForm_SearchTxtBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVProducts
            // 
            this.dGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVProducts.Location = new System.Drawing.Point(15, 51);
            this.dGVProducts.Name = "dGVProducts";
            this.dGVProducts.Size = new System.Drawing.Size(333, 186);
            this.dGVProducts.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "BUSCAR PRODUCTO";
            // 
            // SearchProductForm_SearchTxtBox
            // 
            this.SearchProductForm_SearchTxtBox.Location = new System.Drawing.Point(15, 25);
            this.SearchProductForm_SearchTxtBox.Name = "SearchProductForm_SearchTxtBox";
            this.SearchProductForm_SearchTxtBox.Size = new System.Drawing.Size(333, 20);
            this.SearchProductForm_SearchTxtBox.TabIndex = 4;
            this.SearchProductForm_SearchTxtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchProductForm_SearchTxtBox_KeyDown);
            // 
            // SearchProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 246);
            this.ControlBox = false;
            this.Controls.Add(this.dGVProducts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchProductForm_SearchTxtBox);
            this.Name = "SearchProductForm";
            this.Text = "CATÁLOGO DE PRODUCTOS";
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchProductForm_SearchTxtBox;
    }
}