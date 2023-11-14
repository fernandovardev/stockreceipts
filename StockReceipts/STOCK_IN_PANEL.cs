using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static StockReceipts.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using System.Diagnostics;

namespace StockReceipts
{
    public enum FormMode
    {
        PurchaseMode,
        SearchAndModifyMode
    }

    public partial class SalesRegisterControl : UserControl
    {
        readonly PrinterHelper printerHelper = new PrinterHelper();

        private FormMode currentMode;
        public SalesRegisterControl(int priceMode, int salesPriceMode, FormMode mode)
        {
            InitializeComponent();
            LoadFolVtaValue();
            UpdateLabelsVisibility();
            radioButton1.TabStop = false;
            MAIN_dGV.DoubleBuffered(true);
            branchNumber = Program.LoggedInBranchNumber;
            this.PriceUpdateMode = priceMode;
            this.SalesPriceUpdateMode = salesPriceMode;
            SetFormMode(mode);
            currentMode = mode;
            comboBox1.SelectedItem = "PAGO EN EFECTIVO";
            printerHelper.MessageEvent += PrinterHelper_MessageEvent;
        }
        private void SalesRegisterControl_Load(object sender, EventArgs e)
        {
            if (currentMode == FormMode.SearchAndModifyMode)
            {
                txtboxFOLIO.Focus();
            }
            else
            {
                txtboxPROVEEDOR.Focus();
            }
            AttachGlobalTextBoxEvents(this);
            MAIN_dGV.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }

        public readonly string connectionString = Program.DatabaseConnectionString;
        private readonly string branchNumber = Program.LoggedInBranchNumber;
        public int PriceUpdateMode { get; private set; }
        public int SalesPriceUpdateMode { get; private set; }
        private bool isDataLocked = true;


        private void PrinterHelper_MessageEvent(string message, bool isError)
        {
            this.Invoke((MethodInvoker)delegate {
                if (isError)
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(message, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        private void SetFormMode(FormMode mode)
        {
            currentMode = mode;

            switch (mode)
            {
                case FormMode.PurchaseMode:

                    txtboxFOLIO.Enabled = false;
                    MAIN_dGV.ReadOnly = false;

                    AttachDgvEventHandlers();
                    break;

                case FormMode.SearchAndModifyMode:

                    txtboxFOLIO.Enabled = true;
                    txtboxFOLIO.ReadOnly = false;
                    txtboxFOLIO.Clear();
                    txtboxFOLIO.Focus();

                    txtboxPROVEEDOR.Enabled = false;
                    txtBoxFACTURA.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    comboBox1.Enabled = false;
                    MAIN_txtBox_bonus.Enabled = false;
                    DELETECAMPS_BUTTON.Visible = false;
                    MAIN_dGV.ReadOnly = true;
                    SAVEPURCHASE_lbl.Visible = false;

                    DetachDgvEventHandlers();
                    break;
            }
        }

        private void AttachDgvEventHandlers()
        {
            MAIN_dGV.CellEndEdit += MAIN_dGV_CellEndEdit;
            MAIN_dGV.KeyDown += MAIN_dGV_KeyDown;
            MAIN_dGV.CellValidating += MAIN_dGV_CellValidating;
        }

        private void DetachDgvEventHandlers()
        {
            MAIN_dGV.CellEndEdit -= MAIN_dGV_CellEndEdit;
            MAIN_dGV.KeyDown -= MAIN_dGV_KeyDown;
            MAIN_dGV.CellValidating -= MAIN_dGV_CellValidating;
        }

        public void UnlockDataControls()
        {
            isDataLocked = false;
            MAIN_dGV.Enabled = true;
            MAIN_txtBox_bonus.Enabled = true;
            txtboxPROVEEDOR.Enabled = true;
            txtBoxFACTURA.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void LoadFolVtaValue()
        {
            try
            {
                string tableName = Program.ConstructTableName("Param");
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new OleDbCommand($"SELECT FolCpa FROM {tableName}", connection))
                    {
                        var result = cmd.ExecuteScalar();
                        int nextFolVta = (result != null ? Convert.ToInt32(result) : 0) + 1;
                        txtboxFOLIO.Text = nextFolVta.ToString().PadLeft(10, '0');
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar y preparar el siguiente valor de FolVta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncreaseFolVtaValue()
        {
            try
            {
                string tableName = Program.ConstructTableName("Param");
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    int currentFolVta = int.Parse(txtboxFOLIO.Text);
                    using (var updateCmd = new OleDbCommand($"UPDATE {tableName} SET FolCpa = @currentFolVta", connection))
                    {
                        updateCmd.Parameters.AddWithValue("@currentFolVta", currentFolVta);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al incrementar el valor de FolVta en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private int GetSituacionValue(string situation)
        {
            switch (situation)
            {
                case "PAGO EN EFECTIVO": return 0;
                case "PAGO CON CHEQUE": return 1;
                case "PAGO PENDIENTE": return 2;
                default:
                    MessageBox.Show("Situación inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
            }
        }

        private string DetermineInvoiceOrRemValue()
        {
            if (radioButton1.Checked)
            {
                return txtBoxFACTURA.Text;
            }
            else if (radioButton2.Checked)
            {
                return txtBOXREM.Text;
            }
            return string.Empty;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton1.TabStop = false;
                txtBoxFACTURA.Enabled = true;
                txtBOXREM.Text = string.Empty;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton2.TabStop = false;
                txtBoxFACTURA.Enabled = false;
                txtBoxFACTURA.Text = string.Empty;
                txtBOXREM.Text = "REM-" + txtboxFOLIO.Text;
                UpdateLabelsVisibility();
                if (!string.IsNullOrEmpty(DetermineInvoiceOrRemValue()) && !string.IsNullOrEmpty(txtboxPROVEEDOR.Text))
                {
                    InitializeDataGridView();
                    MAIN_dGV.Focus();
                    if (MAIN_dGV.Rows.Count > 0)
                    {
                        MAIN_dGV.CurrentCell = MAIN_dGV.Rows[0].Cells[0];
                    }
                }
            }
        }

        //////////////////////////////TEXTBOXPROVEEDOR FUNCTIONALITY/////////////////////////////////////////////////

        private void txtboxPROVEEDOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtboxPROVEEDOR.Text = txtboxPROVEEDOR.Text.PadLeft(3, '0');
                string providerCode = txtboxPROVEEDOR.Text.Trim();
                string providerName = FetchProviderNameByCode(providerCode);

                if (providerCode == "000" || string.IsNullOrEmpty(providerName))
                {
                    MessageBox.Show("Código de proveedor no válido o no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true; // Prevent further processing of the keypress
                }
                else
                {
                    lblPROVIDER.Text = providerName;
                }
            }
        }

        private void txtboxPROVEEDOR_Validating(object sender, CancelEventArgs e)
        {
            string providerCode = txtboxPROVEEDOR.Text.Trim().PadLeft(3, '0');
            string providerName = FetchProviderNameByCode(providerCode);

            if (providerCode == "000" || string.IsNullOrEmpty(providerName))
            {
                providerCode = "000";
            }
            txtboxPROVEEDOR.Text = providerCode;
        }

        private void txtboxPROVEEDOR_Validated(object sender, EventArgs e)
        {
            string providerCode = txtboxPROVEEDOR.Text.Trim();
            string providerName = FetchProviderNameByCode(providerCode);
            lblPROVIDER.Text = providerName;
        }

        private void TxtBoxPROVEEDOR_TextChanged(object sender, EventArgs e)
        {
            UpdateLabelsVisibility();
            string providerCode = txtboxPROVEEDOR.Text.Trim();
            if (!string.IsNullOrEmpty(providerCode))
            {
                string providerName = FetchProviderNameByCode(providerCode);
                if (!string.IsNullOrEmpty(providerName))
                {
                    lblPROVIDER.Text = providerName;
                }
            }
        }

        private string FetchProviderNameByCode(string providerCode)
        {
            lblPROVIDER.Visible = true;
            string providerName = string.Empty;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                using (OleDbCommand cmd = new OleDbCommand($"SELECT Descripcion FROM {Program.ConstructTableName("CatProveedor")} WHERE CodProveedor = ?", connection))
                {
                    cmd.Parameters.AddWithValue("@ProviderCode", providerCode);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        providerName = result.ToString();
                        if (providerName.Length > 26)
                        {
                            providerName = providerName.Substring(0, 26);
                        }
                    }
                }
            }

            return providerName;
        }

        private void OpenSearchProviderForm()
        {
            try
            {

                SearchProviderForm searchForm = new SearchProviderForm();
                searchForm.ProviderSelected += (codProveedor, providerName) =>
                {
                    UpdateProviderTextBox(codProveedor);
                    UpdateProviderLabel(providerName);
                };
                searchForm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void UpdateProviderTextBox(string value)
        {
            txtboxPROVEEDOR.Text = value;
            lblPROVIDER.Visible = true;

        }

        public void UpdateProviderLabel(string value)
        {
            lblPROVIDER.Text = value;
            lblPROVIDER.Visible = true;
        }

        ///////////////////////////////////////HOTKEYS////////////////////////////////////////////////
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (currentMode == FormMode.PurchaseMode)
            {
                switch (keyData)
                {
                    case Keys.F1:
                        {
                            if (txtboxPROVEEDOR.Focused)
                            {
                                OpenSearchProviderForm();
                            }
                            else if (MAIN_dGV.Focused || !MAIN_dGV.IsCurrentCellInEditMode)
                            {
                                CheckAndOpenSearchProductForm();
                            }
                            return true;
                        }
                    case Keys.F8:
                        HandleF2KeyPress();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        ///////////////////////////////////////// SEARCH PRODUCT FORM///////////////////////////////////////////////

        public bool CheckAndOpenSearchProductForm()
        {
            if (MAIN_dGV.Focused || MAIN_dGV.IsCurrentCellInEditMode)
            {
                OpenSearchProductForm();
                return true;
            }
            return false;
        }

        private void OpenSearchProductForm()
        {
            SearchProductForm searchForm = new SearchProductForm(txtboxPROVEEDOR.Text);
            searchForm.ProductSelected += (barras) =>
            {
                UpdateDataGridView(barras);
            };
            searchForm.Show();
        }

        public void UpdateDataGridView(string barras)
        {
            var currentRow = MAIN_dGV.CurrentRow;
            if (currentRow != null)
            {
                currentRow.Cells["MAIN_dGV_code"].Value = barras;
                UpdateRowDetails(currentRow.Index);
                MAIN_dGV.CurrentCell = currentRow.Cells[3];

            }
        }
        ////////////////////////////////////////TXTBOXFOLIO////////////////////////////////////////////////////        

        private void TxtBoxFOLIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            MAIN_dGV.Rows.Clear();
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtboxFOLIO.Text = txtboxFOLIO.Text.PadLeft(10, '0');
                if (!FolioExists(txtboxFOLIO.Text))
                {
                    MessageBox.Show("FOLIO NO ENCONTRADO.");
                    return;
                }
                FetchMainPurchaseDetails();
                FetchPurchaseDetailsForGrid();
                UpdateGTOTAL();
                if (HasPermission("MAINMENU_salescancel"))
                {
                    DialogResult result = MessageBox.Show("¿Quieres cancelar la compra?", "Cancelar compra", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        CancelPurchase();
                        return;
                    }
                }
                if (HasPermission("MAINMENU_salesreprint"))
                {
                    DialogResult result = MessageBox.Show("¿Quieres reimprimir el ticket?", "Reimprimir Ticket", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SimpleLogger.Log("User opted to Reprint Ticket.");
                        ReprintTicket();
                        return;
                    }
                }
                else
                {
                    SimpleLogger.Log("User does not have permission to Reprint Ticket.");
                }


                SAVEPURCHASE_lbl.Visible = false;

            }
        }

        private bool FolioExists(string folio)
        {
            string tableName = Program.ConstructTableName("Compras");
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE CodCompra = ?";
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodCompra", folio);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        private void ReprintTicket()
        {
            string ticketContent = FormatTicket($"REIMPRESION DE TICKET DE COMPRA", "", "");
            printerHelper.PrintTicket(ticketContent);
        }

        private void UpdateTotalsAndGTOTAL()
        {
            if (MAIN_dGV == null || MAIN_dGV.Rows.Count == 0) return;

            decimal subtotal = 0, tax = 0;
            if (!TryCalculateTotalsFromGrid(ref subtotal, ref tax)) return;
            MAIN_txtbox_SUBTOTAL.Text = subtotal.ToString("N2");
            MAIN_txtbox_TAX.Text = tax.ToString("N2");
            MAIN_txtbox_TOTAL.Text = (subtotal + tax).ToString("N2");
            UpdateGTOTAL();
        }

        private bool TryCalculateTotalsFromGrid(ref decimal subtotal, ref decimal tax)
        {
            if (!MAIN_dGV.Columns.Contains("MAIN_dGV_imprt") || !MAIN_dGV.Columns.Contains("MAIN_dGV_tax"))
            {
                MessageBox.Show("Required columns are missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (DataGridViewRow row in MAIN_dGV.Rows)
            {
                if (row.IsNewRow) continue;

                decimal.TryParse(row.Cells["MAIN_dGV_imprt"].Value?.ToString(), out decimal import);
                subtotal += import;

                decimal.TryParse(row.Cells["MAIN_dGV_tax"].Value?.ToString(), out decimal rowTax);
                tax += rowTax;


            }

            return true;
        }

        private void UpdateGTOTAL()
        {
            if (!decimal.TryParse(MAIN_txtbox_TOTAL.Text, out decimal total))
            {
                MessageBox.Show("Invalid total value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                total = 0m;
            }

            if (!decimal.TryParse(MAIN_txtBox_bonus.Text, out decimal bonus))
            {
                MessageBox.Show("Invalid bonus value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bonus = 0m;
            }

            bonus = Math.Min(bonus, total);
            decimal gTotal = total - bonus;
            MAIN_txtbox_GTOTAL.Text = gTotal.ToString("N2");
        }


        private bool ValidateInputs()
        {
            List<string> errorMessages = new List<string>();
            if (MAIN_dGV.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
            {
                errorMessages.Add("Debe haber por lo menos un artículo.");
            }
            if (string.IsNullOrEmpty(txtboxPROVEEDOR.Text))
            {
                errorMessages.Add("El campo 'Proveedor' está vacío.");
            }
            if (string.IsNullOrEmpty(DetermineInvoiceOrRemValue()))
            {
                errorMessages.Add("El campo 'Factura' está vacío.");
            }
            if (comboBox1.SelectedItem == null)
            {
                errorMessages.Add("Seleccione una situación.");
            }
            HashSet<string> codes = new HashSet<string>();
            foreach (DataGridViewRow row in MAIN_dGV.Rows)
            {
                if (row.IsNewRow) continue;
                string code = row.Cells["MAIN_dGV_code"].Value?.ToString();
                if (codes.Contains(code))
                {
                    errorMessages.Add($"Código duplicado: {code}");
                    continue;
                }
                codes.Add(code);
                if (!IsRowDataValid(row))
                {
                    errorMessages.Add($"Error en la fila con código: {code}");
                }
            }
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Errores en el formulario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool IsRowDataValid(DataGridViewRow row)
        {
            string code = row.Cells["MAIN_dGV_code"].Value?.ToString();
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            if (!decimal.TryParse(row.Cells["MAIN_dGV_price"].Value?.ToString(), out decimal price) || price <= 0)
            {
                return false;
            }
            if (!decimal.TryParse(row.Cells["MAIN_dGV_imprt"].Value?.ToString(), out decimal import) || import <= 0)
            {
                return false;
            }
            if (!decimal.TryParse(row.Cells["MAIN_dGV_QT"].Value?.ToString(), out decimal quantity) || quantity <= 0)
            {
                return false;
            }
            return true;
        }

        private DataRow GetProductDetailsByBarcode(string barcode)
        {
            DataTable dt = new DataTable();
            string tableName = $"{branchNumber}_CatProductos";
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = $@"SELECT * FROM {tableName} WHERE Barras = @barcode";
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    var adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        //////////////////////////////////////////////////DB INTERACTION//////////////////////////////////////////////

        private bool SaveToCompras()
        {

            string tableName = Program.ConstructTableName("Compras");

            int situacionValue = GetSituacionValue(comboBox1.SelectedItem.ToString());
            if (situacionValue == -1)
                return false;

            try
            {
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = $@"
INSERT INTO {tableName} 
(CodCompra, Fecha, CodProveedor, Subtotal, Iva, Total, Referencia, FechaRef, Estado, FechaBaja, Cajero, CodUsuBaja, Situacion, Bonificacion)
VALUES (@CodCompra, @Fecha, @CodProveedor, @Subtotal, @Iva, @Total, @Referencia, @FechaRef, 'A', @FechaBaja, @USER, '01', @Situacion, @Bonificacion)";

                    using (var cmd = new OleDbCommand(query, connection))
                    {
                        AddParametersToCmd(cmd);
                        SimpleLogger.Log("Executing SaveToCompras with the following parameters:");
                        foreach (OleDbParameter p in cmd.Parameters)
                        {
                            SimpleLogger.Log($"{p.ParameterName} = {p.Value}");
                        }
                        cmd.ExecuteNonQuery();


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SimpleLogger.Log($"Error in SaveToCompras: {ex.Message}");
                MessageBox.Show($"An error occurred while saving row data to ComprasDet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void AddParametersToCmd(OleDbCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CodCompra", txtboxFOLIO.Text.PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@CodProveedor", txtboxPROVEEDOR.Text);
            cmd.Parameters.AddWithValue("@Subtotal", ParseDecimal(MAIN_txtbox_SUBTOTAL.Text, "Valor de subtotal inválido."));
            cmd.Parameters.AddWithValue("@Iva", ParseDecimal(MAIN_txtbox_TAX.Text, "Valor de IVA inválido."));
            cmd.Parameters.AddWithValue("@Total", ParseDecimal(MAIN_txtbox_TOTAL.Text, "Valor de total inválido."));
            cmd.Parameters.AddWithValue("@Referencia", DetermineInvoiceOrRemValue());
            cmd.Parameters.AddWithValue("@FechaRef", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@FechaBaja", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@USER", Program.LoggedInUsername);
            cmd.Parameters.AddWithValue("@Situacion", GetSituacionValue(comboBox1.SelectedItem.ToString()));
            cmd.Parameters.AddWithValue("@Bonificacion", ParseDecimal(MAIN_txtBox_bonus.Text, "Valor de bonificación inválido."));
        }

        private decimal ParseDecimal(string input, string errorMsg)
        {
            if (decimal.TryParse(input, out decimal value))
                return value;
            else
            {
                MessageBox.Show(errorMsg);
                throw new FormatException(errorMsg);
            }
        }

        private bool SaveToComprasDet()
        {
            string comprasDetTableName = Program.ConstructTableName("ComprasDet");
            string catProductosTableName = Program.ConstructTableName("CatProductos");
            bool anyErrorOccurred = false;

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    foreach (DataGridViewRow row in MAIN_dGV.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string barras = row.Cells[0].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(barras))
                        {
                            SimpleLogger.Log("Encountered an empty or null barras value in DataGridView.");
                            continue;
                        }

                        SaveRowToComprasDet(row, comprasDetTableName, catProductosTableName, connection, transaction);
                        decimal qty = Convert.ToDecimal(row.Cells[3].Value);
                        UpdateInventoryInCatProductos(barras, qty, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ocurrió un error al guardar el valor de la fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SimpleLogger.Log($"Error in SaveToComprasDet: {ex.Message}");
                    anyErrorOccurred = true;
                }
            }

            DisplayFeedback();
            return !anyErrorOccurred;
        }


        private void SaveRowToComprasDet(DataGridViewRow row, string comprasDetTableName, string catProductosTableName, OleDbConnection connection, OleDbTransaction transaction)
        {
            string codProducto = FetchCodProducto(row, catProductosTableName, connection, transaction); // Pass the transaction here
            if (string.IsNullOrEmpty(codProducto))
            {
                MessageBox.Show($"Imposible encontrar Producto desde el código de barras: {row.Cells["MAIN_dGV_code"].Value}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $@"
INSERT INTO {comprasDetTableName} 
(CodCompra, CodProducto, Cantidad, Precio, Importe, Iva)
VALUES (@CodCompra, @CodProducto, @Cantidad, @Precio, @Importe, @Iva)";

            using (var cmd = new OleDbCommand(query, connection, transaction))
            {
                AddComprasDetParameters(cmd, row, codProducto);
                cmd.ExecuteNonQuery();
            }
        }


       private string FetchCodProducto(DataGridViewRow row, string catProductosTableName, OleDbConnection connection, OleDbTransaction transaction)
{
    try
    {
        string fetchCodProductoQuery = $"SELECT CodProducto FROM {catProductosTableName} WHERE Barras = ?";
        using (var fetchCmd = new OleDbCommand(fetchCodProductoQuery, connection, transaction)) // Include transaction here
        {
            fetchCmd.Parameters.AddWithValue("@Barras", row.Cells["MAIN_dGV_code"].Value);
            return fetchCmd.ExecuteScalar()?.ToString();
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Failed fetching CodProducto: {ex.Message}", ex);
    }
}



        private void AddComprasDetParameters(OleDbCommand cmd, DataGridViewRow row, string codProducto)
        {
            cmd.Parameters.AddWithValue("@CodCompra", txtboxFOLIO.Text.PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("@CodProducto", codProducto);
            cmd.Parameters.AddWithValue("@Cantidad", row.Cells["MAIN_dGV_QT"].Value);
            cmd.Parameters.AddWithValue("@Precio", row.Cells["MAIN_dGV_price"].Value);
            cmd.Parameters.AddWithValue("@Importe", row.Cells["MAIN_dGV_imprt"].Value);
            cmd.Parameters.AddWithValue("@Iva", row.Cells["MAIN_dGV_tax"].Value);
        }



        private void DisplayFeedback()
        {
            MessageBox.Show("Se ha registrado la compra con éxito.");
        }

        private void UpdatePricesBasedOnUserChoice()
        {
            foreach (DataGridViewRow row in MAIN_dGV.Rows)
            {
                if (row.IsNewRow) continue;

                string barcode = row.Cells["MAIN_dGV_code"].Value.ToString();
                DataRow productDetails = GetProductDetailsByBarcode(barcode);

                if (productDetails == null)
                {
                    MessageBox.Show($"No se encontraron detalles de producto para el código de barras: {barcode}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                decimal pCompra = Convert.ToDecimal(productDetails["PCompra"]);
                decimal iva = Convert.ToDecimal(productDetails["IVA"]);
                decimal elevacion = Convert.ToDecimal(productDetails["Elevacion"]);
                decimal taxRate = Program.LoggedInTaxRate;
                decimal pVenta = Convert.ToDecimal(productDetails["PVenta"]);
                decimal pIva = Convert.ToDecimal(productDetails["PIva"]);

                switch (PriceUpdateMode)
                {
                    case 1:
                        break;
                    case 2:
                        decimal newPCompra = Convert.ToDecimal(row.Cells["MAIN_dGV_price"].Value);
                        if (newPCompra > pCompra)
                            pCompra = newPCompra;
                        break;
                    case 3:
                        pCompra = Convert.ToDecimal(row.Cells["MAIN_dGV_price"].Value);
                        break;
                }

                switch (SalesPriceUpdateMode)
                {
                    case 2:
                        if (iva > 0)
                        {
                            pIva = pCompra * (1 + taxRate)* (1 + elevacion / 100);
                            iva = pCompra * (1+elevacion/100) * taxRate;
                        }
                        else
                        {
                            pVenta = pCompra * (1 + elevacion / 100);

                        }

                        try
                        {
                            UpdateProductDetailsInDatabase(barcode, pCompra, pVenta, pIva, iva);
                            SimpleLogger.Log($"Updated prices for barcode {barcode}. PCompra: {pCompra}, Pventa: {pVenta}, IVA: {iva}");
                        }
                        catch (Exception ex)
                        {
                            SimpleLogger.Log($"Failed to update prices for barcode {barcode}. Error: {ex.Message}");
                            MessageBox.Show($"Falló la actualización de detalles para el producto con código de barras: {barcode}. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    case 1:
                        break;
                }
            }
        }

        private void UpdateProductDetailsInDatabase(string barcode, decimal pCompra, decimal pVenta, decimal pIva, decimal iva)
        {
            string tableName = Program.ConstructTableName("CatProductos");
            string query;

            if (iva > 0)
            {
                // When IVA is greater than 0, update PCompra, Pventa, PIva, and IVA columns
                query = $@"UPDATE {tableName} SET PCompra = @pCompra, Pventa = @pVenta, PIva = @pIva, IVA = @iva WHERE Barras = @barcode";
            }
            else
            {
                // When IVA is 0, update only PCompra and Pventa columns
                query = $@"UPDATE {tableName} SET PCompra = @pCompra, Pventa = @pVenta WHERE Barras = @barcode";
            }

            try
            {
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new OleDbCommand(query, connection))
                    {
                        // Ensure that the decimal values have two decimal places
                        decimal pCompraRounded = Math.Round(pCompra, 2);
                        decimal pVentaRounded = Math.Round(pVenta, 2);
                        decimal pIvaRounded = Math.Round(pIva, 2);
                        decimal ivaRounded = Math.Round(iva, 2);

                        cmd.Parameters.AddWithValue("@pCompra", pCompraRounded);
                        cmd.Parameters.AddWithValue("@pVenta", pVentaRounded);

                        if (iva > 0)
                        {
                            cmd.Parameters.AddWithValue("@pIva", pIvaRounded);
                            cmd.Parameters.AddWithValue("@iva", ivaRounded);
                        }

                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                SimpleLogger.Log($"Failed to execute database update for barcode {barcode}. Error: {ex.Message}");
                throw; // Rethrow the exception for higher-level handling if necessary
            }
        }


        private void HandleF2KeyPress()
        {
            try
            {
                MAIN_txtBox_bonus_Leave(MAIN_txtBox_bonus, EventArgs.Empty);
                SimpleLogger.Log("Initiating Purchase Save Operation");
                LoadFolVtaValue();
                RemoveBlankRow();
                if (ActiveControl is TextBox)
                {
                    var previousControl = ActiveControl;
                    ActiveControl = null;
                    ActiveControl = previousControl;
                }
                if (!ValidateInputs())
                {
                    SimpleLogger.Log("Inputs are invalid. Cancelling operation.");
                    return;
                }

                if (MAIN_dGV.IsCurrentCellInEditMode)
                {
                    MAIN_dGV.EndEdit();
                }

                DialogResult result = MessageBox.Show("¿Estás seguro de que quieres guardar la compra? Esta opción es irreversible.", "Guardar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    IncreaseFolVtaValue();

                    if (SaveToCompras() && SaveToComprasDet())
                    {
                        string ticketContent = FormatTicket("REGISTRO DE COMPRA", "", "");
                        printerHelper.PrintTicket(ticketContent);
                        UpdatePricesBasedOnUserChoice();

                        txtboxPROVEEDOR.Focus();
                        ClearFields();
                        LoadFolVtaValue();
                        MAIN_BillStatus.Visible = false;
                        lblPROVIDER.Text = "";
                        UnlockDataControls();

                        SimpleLogger.Log("Purchase Save Operation completed successfully.");

                    }
                    else
                    {
                        SimpleLogger.Log("Error encountered. Purchase not completed.");
                        MessageBox.Show("Error encontrado. Compra no completada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    SimpleLogger.Log("User cancelled the Purchase Save Operation.");
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.Log($"Error during Purchase Save Operation: {ex.Message}");
                MessageBox.Show($"Error al procesar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void RemoveBlankRow()
        {
            if (MAIN_dGV.Rows.Count > 0)
            {
                var lastRow = MAIN_dGV.Rows[MAIN_dGV.Rows.Count - 1];
                if (IsRowBlank(lastRow))
                {
                    MAIN_dGV.Rows.RemoveAt(MAIN_dGV.Rows.Count - 1);
                }
            }
        }

        private bool IsRowBlank(DataGridViewRow row)
        {

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private void MAIN_txtBox_bonus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateTotalsAndGTOTAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el total general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FetchMainPurchaseDetails()
        {
            string tableName = Program.ConstructTableName("Compras");
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName} WHERE CodCompra = ?";
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodCompra", txtboxFOLIO.Text.PadLeft(10, '0'));

                    {
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtboxPROVEEDOR.Text = reader["CodProveedor"].ToString();
                            lblPROVIDER.Text = FetchProviderNameByCode(txtboxPROVEEDOR.Text);
                            txtBoxFACTURA.Text = reader["Referencia"].ToString();
                            MAIN_txtbox_TAX.Text = reader["Iva"].ToString();
                            MAIN_txtbox_TOTAL.Text = reader["Total"].ToString();
                            MAIN_txtBox_bonus.Text = Convert.ToDecimal(reader["Bonificacion"]).ToString("N2");
                            dateTimePicker1.Value = Convert.ToDateTime(reader["FechaRef"]);
                            txtboxFOLIO.Text = reader["CodCompra"].ToString();
                            int situacionValue = Convert.ToInt32(reader["Situacion"]);
                            string estado = reader["Estado"].ToString();

                            switch (estado)
                            {
                                case "A":
                                    MAIN_BillStatus.Text = "REGISTRADA";
                                    break;
                                case "C":
                                    MAIN_BillStatus.Text = "CANCELADA";
                                    break;
                                default:
                                    MAIN_BillStatus.Text = "UNKNOWN"; // Just a fallback in case there's a data inconsistency.
                                    break;
                            }

                            MAIN_BillStatus.Visible = true; // Ensure it's visible
                            switch (situacionValue)
                            {
                                case 0:
                                    comboBox1.SelectedItem = "PAGO EN EFECTIVO";
                                    break;
                                case 1:
                                    comboBox1.SelectedItem = "PAGO CON CHEQUE";
                                    break;
                                case 2:
                                    comboBox1.SelectedItem = "PAGO PENDIENTE";
                                    break;
                                default:
                                    MessageBox.Show("Situación inválida encontrada en base de datos.");
                                    break;
                            }

                            UpdateTotalsAndGTOTAL();
                        }
                        else
                        {
                            MAIN_BillStatus.Visible = false; // Hide it if no details are found
                        }
                    }
                }
            }
        }



        private void FetchPurchaseDetailsForGrid()
        {
            var codCompra = txtboxFOLIO.Text.PadLeft(10, '0');
            var productDetails = GetDetailsByCodCompra(codCompra);

            if (productDetails.Count == 0)
            {
                MessageBox.Show("No se encontraron productos para dicho Folio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var details in productDetails)
            {
                var barras = GetBarrasByCodProducto(details.CodProducto);
                if (!string.IsNullOrEmpty(barras))
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(MAIN_dGV);
                    newRow.Cells[0].Value = barras;
                    MAIN_dGV.Rows.Add(newRow);
                }
            }

            for (int i = 0; i < MAIN_dGV.Rows.Count; i++)
            {
                var details = productDetails[i];
                UpdateRowDetails(i);
                MAIN_dGV.Rows[i].Cells[3].Value = details.Quantity;
                MAIN_dGV.Rows[i].Cells[4].Value = details.Price;
                MAIN_dGV.Rows[i].Cells[5].Value = details.Importe;
                MAIN_dGV.Rows[i].Cells[6].Value = details.Iva;
            }
            UpdateTotalsAndGTOTAL();
            MAIN_dGV.Refresh();
        }

        string GetBarrasByCodProducto(string codProducto)
        {
            string barras = string.Empty;
            string catProductosTableName = $"{Program.LoggedInBranchNumber}_CatProductos";

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT Barras FROM [{catProductosTableName}] WHERE CodProducto = ?";

                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodProducto", codProducto);

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        barras = reader["Barras"].ToString();
                    }
                }
            }
            return barras;
        }

        private class ProductDetails
        {
            public string CodProducto { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Importe { get; set; }
            public decimal Iva { get; set; }
        }

        private List<ProductDetails> GetDetailsByCodCompra(string codCompra)
        {
            List<ProductDetails> detailsList = new List<ProductDetails>();
            string comprasDetTableName = $"{Program.LoggedInBranchNumber}_ComprasDet";

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT CodProducto, Cantidad, Precio, Importe, Iva FROM [{comprasDetTableName}] WHERE CodCompra = ?";
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodCompra", codCompra.PadLeft(10, '0'));

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        detailsList.Add(new ProductDetails
                        {
                            CodProducto = reader["CodProducto"].ToString(),
                            Quantity = Convert.ToDecimal(reader["Cantidad"]),
                            Price = Convert.ToDecimal(reader["Precio"]),
                            Iva = Convert.ToDecimal(reader["Iva"]),
                            Importe = Convert.ToDecimal(reader["Importe"])
                        });
                    }
                }
            }
            return detailsList;
        }

        private void CancelPurchase()
        {
            try
            {
                SimpleLogger.Log("Initiating Purchase Cancel Operation");
                if (IsBillAlreadyCanceled())
                {
                    MessageBox.Show("Esta factura ya ha sido cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (ConfirmCancellationWithReason(out string cancellationReason))
                        {
                            UpdateBillStatusAndLogCancellation(transaction, cancellationReason);
                            UpdateInventoryBasedOnDataGridView(transaction);

                            transaction.Commit();
                            MAIN_BillStatus.Text = "CANCELADA";
                            MessageBox.Show("La factura ha sido cancelada y el inventario ha sido actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string ticketContent = FormatTicket("CANCELACION DE COMPRA", cancellationReason, "");
                            printerHelper.PrintTicket(ticketContent);
                            SimpleLogger.Log("Purchase Cancel Operation completed successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cancelar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimpleLogger.Log($"Error during Purchase Cancel Operation: {ex.Message}");
            }
        }

        private bool IsBillAlreadyCanceled()
        {
            return MAIN_BillStatus.Text == "CANCELADA";
        }

        private bool ConfirmCancellationWithReason(out string cancellationReason)
        {
            using (CancellationForm cancellationForm = new CancellationForm())
            {
                if (cancellationForm.ShowDialog() == DialogResult.OK)
                {
                    cancellationReason = cancellationForm.CancellationReason;
                    return true;
                }
            }
            cancellationReason = null;
            return false;
        }

        private void UpdateBillStatusAndLogCancellation(OleDbTransaction transaction, string reason)
        {
            try
            {
                string tableName = ConstructTableName("Register");
                UpdateBillStatusToCanceled(transaction);

                SimpleLogger.Log($"Bill cancellation initiated. Reason: {reason}");
                string logActionQuery = $"INSERT INTO {tableName} (Folio, ActionType, [Timestamp], Comments, PerformedBy) VALUES (?, ?, ?, ?, ?)";
                SimpleLogger.Log($"Query: {logActionQuery}");
                using (var cmd = new OleDbCommand(logActionQuery, transaction.Connection, transaction))
                {
                    cmd.Parameters.Add("@Folio", OleDbType.VarWChar).Value = txtboxFOLIO.Text;
                    cmd.Parameters.Add("@ActionType", OleDbType.VarWChar).Value = "Cancellation";
                    cmd.Parameters.Add("@Timestamp", OleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("@Comments", OleDbType.VarWChar).Value = reason;
                    cmd.Parameters.Add("@PerformedBy", OleDbType.VarWChar).Value = Program.LoggedInUsername;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.Log($"Error in UpdateBillStatusAndLogCancellation: {ex.Message}");
                SimpleLogger.Log($"Parameters: Folio={txtboxFOLIO.Text}, ActionType=Cancellation, Timestamp={DateTime.Now}, Comments={reason}, PerformedBy={Program.LoggedInUsername}");
                throw;
            }
        }

        private void UpdateInventoryBasedOnDataGridView(OleDbTransaction transaction)
        {
            foreach (DataGridViewRow row in MAIN_dGV.Rows)
            {
                string barras = row.Cells[0].Value?.ToString();
                if (string.IsNullOrWhiteSpace(barras))
                {
                    SimpleLogger.Log("Encountered an empty or null barras value in DataGridView.");
                    continue;
                }

                if (ProductExistsInCatProductos(barras, transaction))
                {
                    decimal qty = Convert.ToDecimal(row.Cells[3].Value);
                    UpdateInventoryInCatProductos(barras, -qty, transaction);
                }
                else
                {
                    MessageBox.Show($"El producto con código {barras} no existe en CatProductos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool ProductExistsInCatProductos(string barras, OleDbTransaction transaction)
        {
            SimpleLogger.Log($"ProductExistsInCatProductos called with barras: {barras}");

            string catProductosTableName = Program.ConstructTableName("CatProductos");
            string query = $"SELECT COUNT(*) FROM [{catProductosTableName}] WHERE Barras = ?";

            SimpleLogger.Log($"Executing query: {query}");

            using (var cmd = new OleDbCommand(query, transaction.Connection, transaction))
            {
                cmd.Parameters.AddWithValue("@Barras", barras);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                SimpleLogger.Log($"Query result count: {count}");

                return count > 0;
            }
        }

        private void UpdateBillStatusToCanceled(OleDbTransaction transaction)
        {
            string tableName = Program.ConstructTableName("Compras");
            string query = $"UPDATE {tableName} SET Estado = 'C', FechaBaja= Date() WHERE CodCompra = ?";
            using (var cmd = new OleDbCommand(query, transaction.Connection, transaction))
            {
                cmd.Parameters.AddWithValue("@CodCompra", txtboxFOLIO.Text);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateInventoryInCatProductos(string barras, decimal qtyChange, OleDbTransaction transaction)
        {
            string catProductosTableName = Program.ConstructTableName("CatProductos");
            string query = $"UPDATE [{catProductosTableName}] SET Existencia = Existencia + ? WHERE Barras = ?";

            int maxRetries = 3; // Maximum number of retry attempts
            int retryDelay = 1000; // Delay between retries in milliseconds (1 second)

            SimpleLogger.Log($"Attempting to update inventory for product with barras: {barras}. Quantity Change: {qtyChange}, Max Retries: {maxRetries}");

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    using (var cmd = new OleDbCommand(query, transaction?.Connection ?? new OleDbConnection(connectionString), transaction))
                    {
                        cmd.Parameters.Add("@StockChange", OleDbType.Decimal).Value = qtyChange;
                        cmd.Parameters.Add("@Barras", OleDbType.VarChar).Value = barras;

                        // Ensure the connection is open before executing the command.
                        if (cmd.Connection.State != ConnectionState.Open)
                        {
                            cmd.Connection.Open();
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            SimpleLogger.Log($"Successfully updated inventory for product with barras: {barras}. Rows affected: {rowsAffected}");
                            return; // Exit the method successfully
                        }
                        else
                        {
                            SimpleLogger.Log($"No rows were affected when attempting to update inventory for product with barras: {barras}");
                            return; // Exit the method, since no retry is needed
                        }
                    }
                }
                catch (Exception ex)
                {
                    SimpleLogger.Log($"Attempt {attempt} of {maxRetries}: Error updating inventory in CatProductos for product with barras: {barras}. Error: {ex.Message}");

                    if (attempt == maxRetries)
                    {
                        throw; // Rethrow the exception on the last attempt
                    }

                    // Wait before retrying
                    Thread.Sleep(retryDelay);
                }
            }
        }


        private bool UpdateRowDetails(int rowIndex)
        {
            DataGridViewRow row = MAIN_dGV.Rows[rowIndex];
            string codigo = row.Cells["MAIN_dGV_code"].Value?.ToString();

            if (string.IsNullOrEmpty(codigo))
            {
                return false;
            }

            string codProveedor = txtboxPROVEEDOR.Text ?? string.Empty;
            DataTable productDetails = GetProductDetailsByCodigoAndCodProveedor(codigo, codProveedor, rowIndex);

            if (productDetails.Rows.Count > 0)
            {
                DataRow rowDetails = productDetails.Rows[0];
                row.Cells["MAIN_dGV_prodname"].Value = rowDetails["Descripcion"].ToString();
                row.Cells["MAIN_dGV_um"].Value = GetUnidadDescripcion(rowDetails["CodUnidad"].ToString());
                row.Cells["MAIN_dGV_price"].Value = Convert.ToDecimal(rowDetails["PCompra"]).ToString("0.00"); // Formats as a decimal with two decimal places                // Store the Iva value
                decimal ivaRate = Convert.ToDecimal(rowDetails["Iva"]);
                row.Tag = ivaRate;
                return true;
            }
            else
            {
                return false;
            }
        }


        private DataTable GetProductDetailsByCodigoAndCodProveedor(string codigo, string codProveedor, int rowIndex)
        {
            var dt = new DataTable();
            string productsTableName = ConstructTableName("CatProductos");
            string providersTableName = ConstructTableName("CatProveedor");
            string codProducto = string.Empty;

            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string codProductoQuery = $@"SELECT CodProducto FROM {productsTableName} WHERE Barras = ?";
                using (var codProductCmd = new OleDbCommand(codProductoQuery, connection))
                {
                    codProductCmd.Parameters.AddWithValue("?", codigo);
                    codProducto = (string)codProductCmd.ExecuteScalar();

                    SimpleLogger.Log($"Fetched CodProducto: {codProducto} for Barras: {codigo}");
                }
                string checkQuery = $@"SELECT Prod FROM {providersTableName} WHERE CodProveedor = ?";
                using (var checkCmd = new OleDbCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("?", codProveedor);
                    var authorizedProducts = (string)checkCmd.ExecuteScalar();

                    if (authorizedProducts != null && authorizedProducts.Split('-').Contains(codProducto))
                    {
                        SimpleLogger.Log($"Product with CodProducto: {codProducto} is authorized for provider: {codProveedor}");
                    }
                    else
                    {
                        SimpleLogger.Log($"Product with CodProducto: {codProducto} is NOT authorized for provider: {codProveedor}");
                        MessageBox.Show($"El producto {codigo} no está autorizado para el proveedor {codProveedor + "-" + lblPROVIDER.Text}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        MAIN_dGV.Rows[rowIndex].Cells["MAIN_dGV_code"].Value = null;

                        return dt; // Return empty data table since the product is not authorized
                    }
                }
                string query = $@"SELECT Descripcion, CodUnidad, PCompra, Iva FROM {productsTableName} WHERE Barras = ?";
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", codigo);

                    var adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                SimpleLogger.Log($"Executed query: {query} with Codigo: {codigo}");
            }
            return dt;
        }




        private string GetUnidadDescripcion(string codUnidad)
        {
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string tableName = Program.ConstructTableName("CatUnidad");
                const string queryFormat = @"SELECT Descripcion FROM {0} WHERE CodUnidad = @CodUnidad";
                string query = string.Format(queryFormat, tableName);
                using (var cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CodUnidad", codUnidad);
                    return cmd.ExecuteScalar()?.ToString() ?? "N/A";
                }
            }
        }

        private void DELETECAMPS_BUTTON_Click(object sender, EventArgs e)
        {
            txtboxPROVEEDOR.Focus();

            ClearFields();
            MAIN_BillStatus.Visible = false;
            UnlockDataControls();
            lblPROVIDER.Text = "";

        }

        private void ClearFields()
        {
            try
            {
                if (MAIN_dGV.IsCurrentCellInEditMode)
                {
                    MAIN_dGV.CancelEdit();
                }

                MAIN_dGV.CurrentCell = null;
                MAIN_dGV.Rows.Clear();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine("Error clearing DataGridView: " + ex.Message);
            }

            MAIN_txtbox_SUBTOTAL.Text = "0.00";
            MAIN_txtbox_TAX.Text = "0.00";
            MAIN_txtbox_TOTAL.Text = "0.00";
            MAIN_txtbox_GTOTAL.Text = "0.00";
            MAIN_txtBox_bonus.Text = "0.00";
            txtBOXREM.Text = string.Empty;
            txtboxPROVEEDOR.Text = string.Empty;
            txtBoxFACTURA.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Text = "";
            }

            radioButton1.Checked = true;
            radioButton2.Checked = false;
            LoadFolVtaValue();
        }

        private void TxtBoxPROVEEDOR_Enter(object sender, EventArgs e)
        {
            isDataLocked = false;
            SEARCHPROVIDER_lbl.Visible = true;
            lblPROVIDER.Text = " ";
            MAIN_dGV.ClearSelection();
            if (MAIN_dGV.CurrentCell != null)
            {
                MAIN_dGV.CurrentCell.Selected = false;
                MAIN_dGV.Rows.Clear();
            }
            ClearFields();
            MAIN_BillStatus.Visible = false;
            UnlockDataControls();
        }

        private void TxtBoxPROVEEDOR_Leave(object sender, EventArgs e)
        {
            isDataLocked = true;
            SEARCHPROVIDER_lbl.Visible = false;
        }

        private void UpdateLabelsVisibility()
        {
            bool hasProvider = !string.IsNullOrEmpty(txtboxPROVEEDOR.Text);
            bool hasFactura = !string.IsNullOrEmpty(DetermineInvoiceOrRemValue());
            bool hasComboBoxValue = comboBox1.SelectedItem != null;

            SAVEPURCHASE_lbl.Visible = hasProvider && hasFactura && hasComboBoxValue;
        }

        private void CheckAndAddRow()
        {
            if (MAIN_dGV.Rows.Count == 0)
            {
                AddRowSafe();
                return;
            }

            DataGridViewRow lastRow = MAIN_dGV.Rows[MAIN_dGV.Rows.Count - 1];
            bool isRowComplete = lastRow.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()));

            if (isRowComplete)
            {
                AddRowSafe();
            }
        }

        private void AddRowSafe()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(AddRow));
            }
            else
            {
                AddRow();
            }
        }

        private void AddRow()
        {
            MAIN_dGV.Rows.Add();
        }


        private void InitializeDataGridView()
        {
            MAIN_dGV.AllowUserToAddRows = false;
            if (MAIN_dGV.Rows.Count == 0 || IsLastRowCompleted())
            {
                MAIN_dGV.Rows.Add();
            }
        }

        private bool IsLastRowCompleted()
        {
            if (MAIN_dGV.Rows.Count > 0)
            {
                var lastRow = MAIN_dGV.Rows[MAIN_dGV.Rows.Count - 1];
                return IsRowCompleted(lastRow);
            }
            return false;
        }

        private bool IsRowCompleted(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (string.IsNullOrEmpty(cell.Value?.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /////////////////////////////////print ticket/////////////////////////////////////////////


        public class CompanyDetails
        {
            public string RazonSocial { get; set; }
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string Colonia { get; set; }
            public string Ciudad { get; set; }
            public string Rfc { get; set; }
        }

        private CompanyDetails GetCompanyDetails()
        {
            CompanyDetails details = new CompanyDetails();
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CatAguilas";

                using (var cmd = new OleDbCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        details.RazonSocial = reader["RazonSocial"].ToString();
                        details.Nombre = reader["Nombre"].ToString();
                        details.Direccion = reader["Direccion"].ToString();
                        details.Colonia = reader["Colonia"].ToString();
                        details.Ciudad = reader["Ciudad"].ToString();
                        details.Rfc = reader["Rfc"].ToString();
                    }
                }
            }
            return details;
        }

        private string FormatTicket(string ticketType, string reason, string label)
        {
            var companyDetails = GetCompanyDetails();
            string header = FormatHeader(ticketType, companyDetails, reason, label);
            string body = FormatPurchaseDetails();
            string footer = FormatFooter();
            return $"{header}\n{body}\n{footer}";
        }

        private string FormatHeader(string ticketType, CompanyDetails company, string reason, string label)
        {
            string dividerLine = new string('-', 40);
            string separator = new string('*', 40);
            string WrapText(string text, int width)
            {
                var wrappedText = new StringBuilder();
                while (text.Length > width)
                {
                    int spaceIndex = text.LastIndexOf(' ', width);
                    if (spaceIndex == -1) spaceIndex = width;
                    wrappedText.AppendLine(text.Substring(0, spaceIndex));
                    text = text.Substring(spaceIndex).Trim();
                }
                wrappedText.Append(text);
                return wrappedText.ToString();
            }

            string wrappedReason = WrapText(reason, 40);

            return $@"
{company.RazonSocial.Center(40)}
{company.Nombre.Center(40)}
{company.Direccion.Center(40)}
{company.Colonia.PadRight(20) + company.Ciudad.PadRight(20)}
RFC: {company.Rfc.Center(40)}
FECHA: {DateTime.Now:dd-MM-yyyy}. CAJERO: {Program.LoggedInUsername}
HORA: {DateTime.Now:HH:mm:ss}. FOLIO: {txtboxFOLIO.Text}
{separator}
{ticketType.Center(40)}
{label.Center(40)}
{wrappedReason.Center(40)}
{separator}
#FACT: {DetermineInvoiceOrRemValue().PadRight(19)} 
FECHA FACT.: {dateTimePicker1.Value:dd-MM-yyyy}
PROVEEDOR: {lblPROVIDER.Text.PadRight(33).Truncate(33)}
{dividerLine}
CODIGO     PRODUCTO           
CANT.   PRECIO I.V.A.  IMPORTE
{dividerLine}";
        }


        private string FormatFooter()
        {
            string dividerLine = new string('-', 40);
            string blankspace = new string(' ', 40);
            string line = new string('_', 40);


            return $@"
{dividerLine}
 SUBTOTAL: {decimal.Parse(MAIN_txtbox_SUBTOTAL.Text).ToString("N2"),28}
 I.V.A.: {decimal.Parse(MAIN_txtbox_TAX.Text).ToString("N2"),30}
 TOTAL: {decimal.Parse(MAIN_txtbox_TOTAL.Text).ToString("N2"),31}
 BONIFICACION: {decimal.Parse(MAIN_txtBox_bonus.Text).ToString("N2"),24}
 TOTAL: {decimal.Parse(MAIN_txtbox_GTOTAL.Text).ToString("N2"),31}
{blankspace}
{blankspace}
{blankspace}
{line}
 Firma de conformidad:
{blankspace}
{blankspace}
{blankspace}
{blankspace}
{blankspace}
{blankspace}
{blankspace}
";
        }

        private string FormatPurchaseDetails()
        {
            StringBuilder details = new StringBuilder();
            foreach (DataGridViewRow row in MAIN_dGV.Rows)
            {
                string barras = row.Cells["MAIN_dGV_code"].Value?.ToString() ?? string.Empty;
                string codigo = GetCodProductoFromBarras(barras).PadRight(10);
                string producto = (row.Cells["MAIN_dGV_prodname"].Value?.ToString() ?? string.Empty).Truncate(24).PadRight(20);
                string cant = (row.Cells["MAIN_dGV_QT"].Value?.ToString() ?? string.Empty).PadLeft(5);
                string precio = decimal.TryParse(row.Cells["MAIN_dGV_price"].Value?.ToString(), out decimal precioDecimal)
                    ? precioDecimal.ToString("F2").PadLeft(6)
                    : "0.00".PadLeft(6);
                string iva = decimal.TryParse(row.Cells["MAIN_dGV_tax"].Value?.ToString(), out decimal ivaDecimal)
                    ? ivaDecimal.ToString("F2").PadLeft(6)
                    : "0.00".PadLeft(6);
                string importe = decimal.TryParse(row.Cells["MAIN_dGV_imprt"].Value?.ToString(), out decimal importeDecimal)
                    ? importeDecimal.ToString("F2").PadLeft(7)
                    : "0.00".PadLeft(7);
                details.AppendLine($"{codigo} {producto}");
                details.AppendLine($"{cant} {precio} {iva} {importe}");
            }
            return details.ToString();
        }

        private string GetCodProductoFromBarras(string barras)
        {
            string codProducto = string.Empty;
            using (OleDbConnection connection = new OleDbConnection(Program.DatabaseConnectionString))
            {
                connection.Open();

                using (OleDbCommand cmd = new OleDbCommand($"SELECT CodProducto FROM {Program.ConstructTableName("CatProductos")} WHERE Barras = @barras", connection))
                {
                    cmd.Parameters.AddWithValue("@barras", barras);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        codProducto = result.ToString();
                    }
                }
            }

            return codProducto;
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
                if (IsInputValid(txtBox)) // Check if the input is now valid
                {
                    txtBox.BackColor = SystemColors.Window; // Reset to default color
                }
                else
                {
                    txtBox.BackColor = Color.LightYellow;
                }
            }
        }
        private bool IsInputValid(TextBox txtBox)
        {
            return !string.IsNullOrWhiteSpace(txtBox.Text);
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                txtBox.BackColor = SystemColors.Window; // Reset to default color

            }

        }

        private bool isProcessingCellEndEdit = false;

        private void MAIN_dGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isProcessingCellEndEdit) return;
            SimpleLogger.Log($"Starting cell end edit for row {e.RowIndex}, column {e.ColumnIndex}");
            isProcessingCellEndEdit = true;

            if (e.RowIndex < 0 || e.RowIndex >= MAIN_dGV.Rows.Count)
            {
                isProcessingCellEndEdit = false;
                return;
            }

            MAIN_dGV.Rows[e.RowIndex].ErrorText = "";

            try
            {
                if (e.ColumnIndex == 0)
                {
                    SimpleLogger.Log("Processing first column edit.");
                    ProcessFirstColumnEdit(e.RowIndex);
                }
                else if (e.ColumnIndex == 3)
                {
                    SimpleLogger.Log("Processing price or quantity edit.");
                    ProcessPriceOrQuantityEdit(e.RowIndex, e.ColumnIndex);
                }
                else if (e.ColumnIndex == 4)
                {
                    SimpleLogger.Log("Processing tax edit.");
                    UpdateRowImportAndTax(e.RowIndex);
                    ApplyCellStyle(e.RowIndex, e.ColumnIndex);

                    // Apply any cell formatting if necessary for column 4 here.
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.Log($"Error: {ex.Message}");
                SimpleLogger.Log($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show("Error during cell edit: " + ex.Message);
            }
            finally
            {
                SimpleLogger.Log($"Finished cell end edit for row {e.RowIndex}, column {e.ColumnIndex}");
                isProcessingCellEndEdit = false;
            }
        }
        private void ApplyCellStyle(int rowIndex, int columnIndex)
        {
            var cell = MAIN_dGV.Rows[rowIndex].Cells[columnIndex];
            if (decimal.TryParse(cell.Value?.ToString(), out decimal cellValue))
            {
                cell.Value = cellValue.ToString("0.00");
            }
            cell.Style.Font = new Font(MAIN_dGV.Font, FontStyle.Bold);
            cell.Style.ForeColor = Color.Blue;
        }
        private void ProcessPriceOrQuantityEdit(int rowIndex, int columnIndex)
        {
            // Update import and tax regardless of which column is edited.
            UpdateRowImportAndTax(rowIndex);
            CheckAndAddRow();

            // Only move to the next row if the third column is edited.
            if (columnIndex == 3)
            {
                BeginInvoke(new Action(() =>
                {
                    if (rowIndex < MAIN_dGV.Rows.Count)
                    {
                        MoveToNextRowFirstColumn(rowIndex);
                    }
                }));
            }
        }

        // Rest of the supporting methods like MoveToNextRowFirstColumn and SetFocusAndBeginEdit remain the same.

        private void ProcessFirstColumnEdit(int rowIndex)
        {
            if (string.IsNullOrWhiteSpace(MAIN_dGV.Rows[rowIndex].Cells[0].Value?.ToString()))
            {
                MoveToNextCell(rowIndex); // Define this method to move to the next cell
            }
            else
            {
                bool updated = UpdateRowDetails(rowIndex);
                int targetCellIndex = updated ? 3 : 0;

                BeginInvoke(new Action(() =>
                {
                    if (rowIndex < MAIN_dGV.Rows.Count)
                    {
                        SetFocusAndBeginEdit(rowIndex, targetCellIndex);
                    }
                }));
            }
        }
        private void MoveToNextCell(int rowIndex)
        {
            BeginInvoke(new Action(() =>
            {
                if (rowIndex + 1 < MAIN_dGV.Rows.Count)
                {
                    SetFocusAndBeginEdit(rowIndex + 1, 0);
                }
            }));
        }
        private void SetFocusAndBeginEdit(int rowIndex, int cellIndex)
        {
            MAIN_dGV.CurrentCell = MAIN_dGV.Rows[rowIndex].Cells[cellIndex];

            if (!MAIN_dGV.CurrentCell.Displayed)
            {
                MAIN_dGV.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }
        private void MAIN_dGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    e.Cancel = false;
                }
                else
                {
                }
            }
        }
        private void UpdateRowImportAndTax(int rowIndex)
        {
            var row = MAIN_dGV.Rows[rowIndex];
            decimal import = 0, tax = 0;

            decimal ivaRate = (decimal)row.Tag; 

            if (decimal.TryParse(row.Cells["MAIN_dGV_price"].Value?.ToString(), out decimal price) &&
                decimal.TryParse(row.Cells["MAIN_dGV_QT"].Value?.ToString(), out decimal qty) && qty > 0)
            {
                import = price * qty;
                if (ivaRate > 0)
                {
                    tax = import * Program.LoggedInTaxRate;
                }
            }

            row.Cells["MAIN_dGV_imprt"].Value = import.ToString("N2");
            row.Cells["MAIN_dGV_tax"].Value = tax.ToString("N2");

            UpdateTotalsAndGTOTAL();
        }
        private void MoveToNextRowFirstColumn(int currentRow)
        {
            if (currentRow + 1 < MAIN_dGV.RowCount)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MAIN_dGV.CurrentCell = MAIN_dGV.Rows[currentRow + 1].Cells[0];
                    MAIN_dGV.BeginEdit(true);
                }));
            }
        }
        private void MAIN_dGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteCurrentRow();
                UpdateTotalsAndGTOTAL();

            }

        }
        private void DeleteCurrentRow()
        {
            try
            {
                if (MAIN_dGV.CurrentCell != null)
                {
                    int rowIndex = MAIN_dGV.CurrentCell.RowIndex;
                    if (rowIndex >= 0 && !MAIN_dGV.Rows[rowIndex].IsNewRow)
                    {
                        MAIN_dGV.Rows.RemoveAt(rowIndex);
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Ocurrió un error al intetar borrar la columna: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                // Handle other types of exceptions if necessary
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                UpdateLabelsVisibility();
                InitializeDataGridView();
                await Task.Delay(50);  // short delay
                MAIN_dGV.CurrentCell = MAIN_dGV.Rows[0].Cells[0];
                return;
            }
        }
        private void txtBoxFACTURA_Leave(object sender, EventArgs e)
        {

            txtBoxFACTURA.Text = txtBoxFACTURA.Text.ToUpper();
            UpdateLabelsVisibility();

        }

        private void MAIN_dGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Check if the edit is in the code column
            if (e.ColumnIndex == 0)
            {
                DataGridViewRow currentRow = MAIN_dGV.Rows[e.RowIndex];

                // Clear the quantity, import, and tax cells
                currentRow.Cells[1].Value = DBNull.Value;
                currentRow.Cells[2].Value = DBNull.Value;
                currentRow.Cells[3].Value = DBNull.Value;
                currentRow.Cells[4].Value = DBNull.Value;
                currentRow.Cells[5].Value = DBNull.Value;
                currentRow.Cells[6].Value = DBNull.Value;
                UpdateTotalsAndGTOTAL();


            }
        }

        private void MAIN_txtBox_bonus_Leave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(MAIN_txtBox_bonus.Text, out decimal bonus))
            {
                bonus = 0.00m;
            }
            MAIN_txtBox_bonus.Text = bonus.ToString("0.00");
        }

    }
    public class PrinterHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]

        private static extern SafeFileHandle CreateFile(
            string lpFileName,
            FileAccess dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            FileMode dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        private const string Lpt1 = "LPT1";
        public delegate void MessageEventHandler(string message, bool isError);
        public event MessageEventHandler MessageEvent;
        protected virtual void OnMessageEvent(string message, bool isError)
        {
            MessageEvent?.Invoke(message, isError);
        }
        public void PrintTicket(string content)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            Task.Run(() =>
            {
                try
                {
                    SimpleLogger.Log("Starting print task.");

                    using (SafeFileHandle fileHandle = CreateFile(Lpt1, FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero))
                    {
                        if (fileHandle.IsInvalid)
                        {
                            SimpleLogger.Log("Failed to access LPT1 port: Invalid file handle.");
                            throw new InvalidOperationException("Failed to access LPT1 port.");
                        }

                        SimpleLogger.Log("Accessed LPT1 port successfully.");

                        using (FileStream fileStream = new FileStream(fileHandle, FileAccess.ReadWrite))
                        {
                            byte[] bytes = Encoding.ASCII.GetBytes(content);
                            fileStream.Write(bytes, 0, bytes.Length);
                        }
                    }

                    SimpleLogger.Log("Print task completed successfully.");
                    OnMessageEvent("Ticket impreso sin problemas", false);
                }
                catch (Exception ex)
                {
                    SimpleLogger.Log($"Error during printing: {ex.Message}");
                    OnMessageEvent("Error al imprimir ticket: " + ex.Message, true);
                }
            }, cts.Token).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    SimpleLogger.Log("Printing operation timed out.");
                    OnMessageEvent("Error al imprimir ticket", true);
                }
            });
        }

    }
    public static class StringExtensions
    {
        public static string Center(this string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)
                                         + stringToCenter.Length)
                                 .PadRight(totalLength);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

    }
}

