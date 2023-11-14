using System;
using System.Data;
using System.Windows.Forms;

namespace StockReceipts
{
    public partial class SearchProductForm : Form
    {
        string _providerCode;

        public SearchProductForm(string providerCode)
        {
            _providerCode = providerCode; 
            InitializeComponent();
            dGVProducts.KeyDown += dGVProducts_KeyDown;
            dGVProducts.CellDoubleClick += dGVProducts_CellDoubleClick;
            InitializeDataGridViewColumns();
            SearchProductForm_SearchTxtBox.TextChanged += SearchProductForm_SearchTxtBox_TextChanged;
            SearchProductForm_SearchTxtBox.Focus();
        }
        public event Action<string> ProductSelected;

        private void InitializeDataGridViewColumns()
        {
            // Clear existing columns
            dGVProducts.Columns.Clear();

            // Add Barras column
            var barrasColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Barras", // This should match the actual data source column name
                HeaderText = "COD.BARRAS",
                Name = "dGV_Barras",
            };
            dGVProducts.Columns.Add(barrasColumn);

            // Add Descripcion column
            var descripcionColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion", // This should match the actual data source column name
                HeaderText = "DESCRIPCIÓN",
                Name = "dGV_Descripcion",
                Width=206
            };
            dGVProducts.Columns.Add(descripcionColumn);
        }

        private void SelectProductFromDataGridView()
        {
            if (dGVProducts.CurrentRow != null)
            {
                var row = dGVProducts.CurrentRow;
                string barras = Convert.ToString(row.Cells["dGV_Barras"].Value);

                ProductSelected?.Invoke(barras);

                Close();
            }
        }


        private void SearchProductForm_SearchTxtBox_TextChanged(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            string searchTerm = SearchProductForm_SearchTxtBox.Text.Trim();
            dGVProducts.DataSource = FetchProductsByNameAndProvider(searchTerm, _providerCode);
        }


        private DataTable FetchProductsByNameAndProvider(string searchTerm, string providerCode)
        {
            DataTable dt = new DataTable();
            using (var connection = new System.Data.OleDb.OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                // First, fetch the concatenated product codes for the given provider
                string prodCodesConcatenated = "";
                using (var cmdProvider = new System.Data.OleDb.OleDbCommand($"SELECT Prod FROM {Program.ConstructTableName("CatProveedor")} WHERE CodProveedor = ?", connection))
                {
                    cmdProvider.Parameters.AddWithValue("@ProviderCode", providerCode);
                    using (var reader = cmdProvider.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            prodCodesConcatenated = reader.GetString(0);
                        }
                    }
                }

                // Parse the concatenated string to get individual product codes
                var prodCodes = prodCodesConcatenated.Split('-');

                // Now, fetch the products by name and product codes
                using (var cmdProducts = new System.Data.OleDb.OleDbCommand($"SELECT Barras, Descripcion FROM {Program.ConstructTableName("CatProductos")} WHERE Descripcion LIKE ? AND CodProducto IN ('" + string.Join("','", prodCodes) + "')", connection))
                {
                    cmdProducts.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    new System.Data.OleDb.OleDbDataAdapter(cmdProducts).Fill(dt);
                }
            }
            return dt;
        }


        private void dGVProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProductFromDataGridView();
        }

        private void dGVProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProductFromDataGridView();
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_ACTIVATE = 0x0006;
            const int WA_INACTIVE = 0;
            if (m.Msg == WM_ACTIVATE && ((int)m.WParam & 0xFFFF) == WA_INACTIVE)
            {
                Close(); // Close the form if it loses focus
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void SearchProductForm_Load(object sender, EventArgs e)
        {
            SearchProductForm_SearchTxtBox.Focus();
        }

        private void SearchProductForm_Leave(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchProductForm_SearchTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                dGVProducts.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                dGVProducts.Focus();
                // Optionally, select the first row in the DataGridView if not already selected.
                if (dGVProducts.Rows.Count > 0)
                {
                    dGVProducts.CurrentCell = dGVProducts.Rows[0].Cells[0];
                }
            }
        }

    }
}
