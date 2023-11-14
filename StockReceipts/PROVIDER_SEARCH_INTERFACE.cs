using System;
using System.Data;
using System.Windows.Forms;

namespace StockReceipts
{
    public partial class SearchProviderForm : Form
    {
        private string connectionString = Program.DatabaseConnectionString;

        public event Action<string, string> ProviderSelected;

        public SearchProviderForm()
        {
            InitializeComponent();
            dGVProvider.KeyDown += dGVProvider_KeyDown;
            dGVProvider.CellDoubleClick += dGVProvider_CellDoubleClick;
            InitializeDataGridViewColumns();
            SearchProviderForm_SearchTxtBox.TextChanged += SearchProviderForm_SearchTxtBox_TextChanged;
            SearchProviderForm_SearchTxtBox.Focus();
        }
        private void InitializeDataGridViewColumns()
        {
            // Clear existing columns
            dGVProvider.Columns.Clear();

            // Add CodProveedor column
            var codProveedorColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodProveedor", // This should match the actual data source column name
                HeaderText = "Code",
                Name = "dGV_CodProveedor"
            };
            dGVProvider.Columns.Add(codProveedorColumn);

            // Add Description column
            var descriptionColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion", // This should match the actual data source column name
                HeaderText = "Description",
                Name = "dGV_Description",
                Width=206
            };
            dGVProvider.Columns.Add(descriptionColumn);

            // Optionally, you can set other column properties such as width, formatting, etc.
        }
        private void SelectProviderFromDataGridView()
        {
            if (dGVProvider.CurrentRow != null)
            {
                var row = dGVProvider.CurrentRow;
                string codProveedor = Convert.ToString(row.Cells["dGV_CodProveedor"].Value);
                string providerName = Convert.ToString(row.Cells["dGV_Description"].Value);

                providerName = providerName.Length > 26 ? providerName.Substring(0, 26) : providerName;
                ProviderSelected?.Invoke(codProveedor, providerName);

                Close();
            }
        }
        private void SearchProviderForm_SearchTxtBox_TextChanged(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            string searchTerm = SearchProviderForm_SearchTxtBox.Text.Trim();
            dGVProvider.DataSource = FetchProvidersByName(searchTerm);
        }

        private DataTable FetchProvidersByName(string searchTerm)
        {
            DataTable dt = new DataTable();
            using (var connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new System.Data.OleDb.OleDbCommand($"SELECT CodProveedor, Descripcion FROM {Program.ConstructTableName("CatProveedor")} WHERE Descripcion LIKE ?", connection))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    new System.Data.OleDb.OleDbDataAdapter(cmd).Fill(dt);
                }
            }

            TrimDescriptionColumn(dt);
            return dt;
        }

        private void TrimDescriptionColumn(DataTable dt)
        {
            if (dt.Columns.Contains("Descripcion"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string description = row["Descripcion"].ToString();
                    if (description.Length > 26)
                    {
                        row["Descripcion"] = description.Substring(0, 26);
                    }
                }
            }
        }

        private void dGVProvider_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProviderFromDataGridView();
        }

        private void dGVProvider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SelectProviderFromDataGridView();
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

        private void SearchProviderForm_Load(object sender, EventArgs e)
        {
            SearchProviderForm_SearchTxtBox.Focus();
        }

        private void SearchProviderForm_Leave(object sender, EventArgs e)
        {
            Close();
        }
    }
}
