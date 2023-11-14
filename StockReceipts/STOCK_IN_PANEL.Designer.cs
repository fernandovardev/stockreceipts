using System.Windows.Forms;

namespace StockReceipts
{
    partial class SalesRegisterControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.catProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MAIN_BillStatus = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MAIN_txtBox_bonus = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MAIN_txtbox_GTOTAL = new System.Windows.Forms.Label();
            this.MAIN_txtbox_TOTAL = new System.Windows.Forms.Label();
            this.MAIN_txtbox_TAX = new System.Windows.Forms.Label();
            this.MAIN_txtbox_SUBTOTAL = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtBOXREM = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtBoxFACTURA = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPROVIDER = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxPROVEEDOR = new System.Windows.Forms.TextBox();
            this.txtboxFOLIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DELETECAMPS_BUTTON = new System.Windows.Forms.Button();
            this.SEARCHPROVIDER_lbl = new System.Windows.Forms.Label();
            this.SAVEPURCHASE_lbl = new System.Windows.Forms.Label();
            this.MAIN_dGV = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MAIN_dGV_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAIN_dGV_prodname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main_dGV_um = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAIN_dGV_QT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAIN_dGV_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAIN_dGV_imprt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAIN_dGV_tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.catProductosBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MAIN_dGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.MAIN_BillStatus);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.txtboxFOLIO);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 177);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // MAIN_BillStatus
            // 
            this.MAIN_BillStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MAIN_BillStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MAIN_BillStatus.Location = new System.Drawing.Point(513, 17);
            this.MAIN_BillStatus.MaxLength = 15;
            this.MAIN_BillStatus.Name = "MAIN_BillStatus";
            this.MAIN_BillStatus.ReadOnly = true;
            this.MAIN_BillStatus.Size = new System.Drawing.Size(110, 13);
            this.MAIN_BillStatus.TabIndex = 40;
            this.MAIN_BillStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MAIN_BillStatus.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.MAIN_txtBox_bonus);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.MAIN_txtbox_GTOTAL);
            this.groupBox2.Controls.Add(this.MAIN_txtbox_TOTAL);
            this.groupBox2.Controls.Add(this.MAIN_txtbox_TAX);
            this.groupBox2.Controls.Add(this.MAIN_txtbox_SUBTOTAL);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(316, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 132);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Totales";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(36, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 2);
            this.panel1.TabIndex = 29;
            // 
            // MAIN_txtBox_bonus
            // 
            this.MAIN_txtBox_bonus.Location = new System.Drawing.Point(131, 78);
            this.MAIN_txtBox_bonus.Name = "MAIN_txtBox_bonus";
            this.MAIN_txtBox_bonus.Size = new System.Drawing.Size(102, 20);
            this.MAIN_txtBox_bonus.TabIndex = 0;
            this.MAIN_txtBox_bonus.TabStop = false;
            this.MAIN_txtBox_bonus.Text = "0.00";
            this.MAIN_txtBox_bonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MAIN_txtBox_bonus.TextChanged += new System.EventHandler(this.MAIN_txtBox_bonus_TextChanged);
            this.MAIN_txtBox_bonus.Leave += new System.EventHandler(this.MAIN_txtBox_bonus_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label11.Location = new System.Drawing.Point(49, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 25);
            this.label11.TabIndex = 27;
            this.label11.Text = "TOTAL";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "TOTAL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(91, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "IVA";
            // 
            // MAIN_txtbox_GTOTAL
            // 
            this.MAIN_txtbox_GTOTAL.AutoSize = true;
            this.MAIN_txtbox_GTOTAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MAIN_txtbox_GTOTAL.ForeColor = System.Drawing.SystemColors.Highlight;
            this.MAIN_txtbox_GTOTAL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MAIN_txtbox_GTOTAL.Location = new System.Drawing.Point(181, 100);
            this.MAIN_txtbox_GTOTAL.Name = "MAIN_txtbox_GTOTAL";
            this.MAIN_txtbox_GTOTAL.Size = new System.Drawing.Size(50, 25);
            this.MAIN_txtbox_GTOTAL.TabIndex = 24;
            this.MAIN_txtbox_GTOTAL.Text = "0.00";
            this.MAIN_txtbox_GTOTAL.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MAIN_txtbox_TOTAL
            // 
            this.MAIN_txtbox_TOTAL.AutoSize = true;
            this.MAIN_txtbox_TOTAL.Location = new System.Drawing.Point(206, 61);
            this.MAIN_txtbox_TOTAL.Name = "MAIN_txtbox_TOTAL";
            this.MAIN_txtbox_TOTAL.Size = new System.Drawing.Size(28, 13);
            this.MAIN_txtbox_TOTAL.TabIndex = 23;
            this.MAIN_txtbox_TOTAL.Text = "0.00";
            this.MAIN_txtbox_TOTAL.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MAIN_txtbox_TAX
            // 
            this.MAIN_txtbox_TAX.AutoSize = true;
            this.MAIN_txtbox_TAX.Location = new System.Drawing.Point(206, 38);
            this.MAIN_txtbox_TAX.Name = "MAIN_txtbox_TAX";
            this.MAIN_txtbox_TAX.Size = new System.Drawing.Size(28, 13);
            this.MAIN_txtbox_TAX.TabIndex = 22;
            this.MAIN_txtbox_TAX.Text = "0.00";
            this.MAIN_txtbox_TAX.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MAIN_txtbox_SUBTOTAL
            // 
            this.MAIN_txtbox_SUBTOTAL.AutoSize = true;
            this.MAIN_txtbox_SUBTOTAL.Location = new System.Drawing.Point(206, 17);
            this.MAIN_txtbox_SUBTOTAL.Name = "MAIN_txtbox_SUBTOTAL";
            this.MAIN_txtbox_SUBTOTAL.Size = new System.Drawing.Size(28, 13);
            this.MAIN_txtbox_SUBTOTAL.TabIndex = 21;
            this.MAIN_txtbox_SUBTOTAL.Text = "0.00";
            this.MAIN_txtbox_SUBTOTAL.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "BONIFICACIÓN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "SUBTOTAL";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.txtBOXREM);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.txtBoxFACTURA);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblPROVIDER);
            this.groupBox4.Controls.Add(this.dateTimePicker1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtboxPROVEEDOR);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(3, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(313, 158);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos de Factura";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(32, 76);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(78, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "REMISION";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // txtBOXREM
            // 
            this.txtBOXREM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBOXREM.Enabled = false;
            this.txtBOXREM.Location = new System.Drawing.Point(113, 73);
            this.txtBOXREM.MaxLength = 15;
            this.txtBOXREM.Name = "txtBOXREM";
            this.txtBOXREM.ReadOnly = true;
            this.txtBOXREM.Size = new System.Drawing.Size(139, 20);
            this.txtBOXREM.TabIndex = 2;
            this.txtBOXREM.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(32, 46);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(75, 17);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "FACTURA";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // txtBoxFACTURA
            // 
            this.txtBoxFACTURA.Location = new System.Drawing.Point(113, 45);
            this.txtBoxFACTURA.MaxLength = 15;
            this.txtBoxFACTURA.Name = "txtBoxFACTURA";
            this.txtBoxFACTURA.Size = new System.Drawing.Size(139, 20);
            this.txtBoxFACTURA.TabIndex = 1;
            this.txtBoxFACTURA.Leave += new System.EventHandler(this.txtBoxFACTURA_Leave);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "PAGO EN EFECTIVO";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PAGO EN EFECTIVO",
            "PAGO CON CHEQUE",
            "PAGO PENDIENTE"});
            this.comboBox1.Location = new System.Drawing.Point(113, 129);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Tag = "";
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "FECHA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "SITUACIÓN";
            // 
            // lblPROVIDER
            // 
            this.lblPROVIDER.AutoSize = true;
            this.lblPROVIDER.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPROVIDER.ForeColor = System.Drawing.Color.Maroon;
            this.lblPROVIDER.Location = new System.Drawing.Point(161, 20);
            this.lblPROVIDER.Name = "lblPROVIDER";
            this.lblPROVIDER.Size = new System.Drawing.Size(0, 13);
            this.lblPROVIDER.TabIndex = 2;
            this.lblPROVIDER.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPROVIDER.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 101);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(139, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "PROVEEDOR";
            // 
            // txtboxPROVEEDOR
            // 
            this.txtboxPROVEEDOR.BackColor = System.Drawing.Color.White;
            this.txtboxPROVEEDOR.Location = new System.Drawing.Point(113, 17);
            this.txtboxPROVEEDOR.MaxLength = 3;
            this.txtboxPROVEEDOR.Name = "txtboxPROVEEDOR";
            this.txtboxPROVEEDOR.Size = new System.Drawing.Size(38, 20);
            this.txtboxPROVEEDOR.TabIndex = 0;
            this.txtboxPROVEEDOR.Text = "000";
            this.txtboxPROVEEDOR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtboxPROVEEDOR.Enter += new System.EventHandler(this.TxtBoxPROVEEDOR_Enter);
            this.txtboxPROVEEDOR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxPROVEEDOR_KeyPress);
            this.txtboxPROVEEDOR.Leave += new System.EventHandler(this.TxtBoxPROVEEDOR_Leave);
            this.txtboxPROVEEDOR.Validating += new System.ComponentModel.CancelEventHandler(this.txtboxPROVEEDOR_Validating);
            this.txtboxPROVEEDOR.Validated += new System.EventHandler(this.txtboxPROVEEDOR_Validated);
            // 
            // txtboxFOLIO
            // 
            this.txtboxFOLIO.Enabled = false;
            this.txtboxFOLIO.Location = new System.Drawing.Point(404, 13);
            this.txtboxFOLIO.MaxLength = 10;
            this.txtboxFOLIO.Name = "txtboxFOLIO";
            this.txtboxFOLIO.ReadOnly = true;
            this.txtboxFOLIO.Size = new System.Drawing.Size(103, 20);
            this.txtboxFOLIO.TabIndex = 12;
            this.txtboxFOLIO.TabStop = false;
            this.txtboxFOLIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtboxFOLIO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBoxFOLIO_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(360, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "FOLIO";
            // 
            // DELETECAMPS_BUTTON
            // 
            this.DELETECAMPS_BUTTON.BackColor = System.Drawing.Color.IndianRed;
            this.DELETECAMPS_BUTTON.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DELETECAMPS_BUTTON.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DELETECAMPS_BUTTON.Location = new System.Drawing.Point(513, 4);
            this.DELETECAMPS_BUTTON.Name = "DELETECAMPS_BUTTON";
            this.DELETECAMPS_BUTTON.Size = new System.Drawing.Size(110, 32);
            this.DELETECAMPS_BUTTON.TabIndex = 0;
            this.DELETECAMPS_BUTTON.TabStop = false;
            this.DELETECAMPS_BUTTON.Text = "NUEVA COMPRA";
            this.DELETECAMPS_BUTTON.UseVisualStyleBackColor = false;
            this.DELETECAMPS_BUTTON.Click += new System.EventHandler(this.DELETECAMPS_BUTTON_Click);
            // 
            // SEARCHPROVIDER_lbl
            // 
            this.SEARCHPROVIDER_lbl.AutoSize = true;
            this.SEARCHPROVIDER_lbl.BackColor = System.Drawing.Color.White;
            this.SEARCHPROVIDER_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SEARCHPROVIDER_lbl.ForeColor = System.Drawing.Color.Black;
            this.SEARCHPROVIDER_lbl.Location = new System.Drawing.Point(4, 14);
            this.SEARCHPROVIDER_lbl.Name = "SEARCHPROVIDER_lbl";
            this.SEARCHPROVIDER_lbl.Size = new System.Drawing.Size(160, 13);
            this.SEARCHPROVIDER_lbl.TabIndex = 1;
            this.SEARCHPROVIDER_lbl.Text = "F1:BUSCAR PROVEEDOR ";
            this.SEARCHPROVIDER_lbl.Visible = false;
            this.SEARCHPROVIDER_lbl.Enter += new System.EventHandler(this.TxtBoxPROVEEDOR_Enter);
            // 
            // SAVEPURCHASE_lbl
            // 
            this.SAVEPURCHASE_lbl.AutoSize = true;
            this.SAVEPURCHASE_lbl.BackColor = System.Drawing.Color.White;
            this.SAVEPURCHASE_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAVEPURCHASE_lbl.ForeColor = System.Drawing.Color.Black;
            this.SAVEPURCHASE_lbl.Location = new System.Drawing.Point(170, 14);
            this.SAVEPURCHASE_lbl.Name = "SAVEPURCHASE_lbl";
            this.SAVEPURCHASE_lbl.Size = new System.Drawing.Size(146, 13);
            this.SAVEPURCHASE_lbl.TabIndex = 0;
            this.SAVEPURCHASE_lbl.Text = "F8: GUARDAR COMPRA";
            // 
            // MAIN_dGV
            // 
            this.MAIN_dGV.AllowUserToAddRows = false;
            this.MAIN_dGV.AllowUserToResizeRows = false;
            this.MAIN_dGV.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MAIN_dGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MAIN_dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MAIN_dGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAIN_dGV_code,
            this.MAIN_dGV_prodname,
            this.main_dGV_um,
            this.MAIN_dGV_QT,
            this.MAIN_dGV_price,
            this.MAIN_dGV_imprt,
            this.MAIN_dGV_tax});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MAIN_dGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.MAIN_dGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MAIN_dGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MAIN_dGV.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.MAIN_dGV.Location = new System.Drawing.Point(0, 0);
            this.MAIN_dGV.Name = "MAIN_dGV";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MAIN_dGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MAIN_dGV.RowHeadersWidth = 20;
            this.MAIN_dGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.MAIN_dGV.Size = new System.Drawing.Size(646, 285);
            this.MAIN_dGV.StandardTab = true;
            this.MAIN_dGV.TabIndex = 0;
            this.MAIN_dGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.MAIN_dGV_CellBeginEdit);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Crimson;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(646, 510);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 38;
            this.splitContainer1.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.MAIN_dGV);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel2.Controls.Add(this.SEARCHPROVIDER_lbl);
            this.splitContainer2.Panel2.Controls.Add(this.SAVEPURCHASE_lbl);
            this.splitContainer2.Panel2.Controls.Add(this.DELETECAMPS_BUTTON);
            this.splitContainer2.Size = new System.Drawing.Size(646, 331);
            this.splitContainer2.SplitterDistance = 285;
            this.splitContainer2.TabIndex = 39;
            this.splitContainer2.TabStop = false;
            // 
            // MAIN_dGV_code
            // 
            this.MAIN_dGV_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAIN_dGV_code.HeaderText = "CÓDIGO";
            this.MAIN_dGV_code.MaxInputLength = 13;
            this.MAIN_dGV_code.Name = "MAIN_dGV_code";
            this.MAIN_dGV_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_code.Width = 55;
            // 
            // MAIN_dGV_prodname
            // 
            this.MAIN_dGV_prodname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MAIN_dGV_prodname.HeaderText = "NOMBRE DEL PRODUCTO";
            this.MAIN_dGV_prodname.MaxInputLength = 50;
            this.MAIN_dGV_prodname.Name = "MAIN_dGV_prodname";
            this.MAIN_dGV_prodname.ReadOnly = true;
            this.MAIN_dGV_prodname.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MAIN_dGV_prodname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_prodname.Width = 206;
            // 
            // main_dGV_um
            // 
            this.main_dGV_um.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.main_dGV_um.HeaderText = "U.M.";
            this.main_dGV_um.MaxInputLength = 10;
            this.main_dGV_um.Name = "main_dGV_um";
            this.main_dGV_um.ReadOnly = true;
            this.main_dGV_um.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.main_dGV_um.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.main_dGV_um.Width = 36;
            // 
            // MAIN_dGV_QT
            // 
            this.MAIN_dGV_QT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MAIN_dGV_QT.HeaderText = "CANT";
            this.MAIN_dGV_QT.Name = "MAIN_dGV_QT";
            this.MAIN_dGV_QT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_QT.Width = 42;
            // 
            // MAIN_dGV_price
            // 
            this.MAIN_dGV_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MAIN_dGV_price.HeaderText = "PRECIO";
            this.MAIN_dGV_price.Name = "MAIN_dGV_price";
            this.MAIN_dGV_price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_price.Width = 53;
            // 
            // MAIN_dGV_imprt
            // 
            this.MAIN_dGV_imprt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MAIN_dGV_imprt.HeaderText = "IMPORTE";
            this.MAIN_dGV_imprt.Name = "MAIN_dGV_imprt";
            this.MAIN_dGV_imprt.ReadOnly = true;
            this.MAIN_dGV_imprt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_imprt.Width = 62;
            // 
            // MAIN_dGV_tax
            // 
            this.MAIN_dGV_tax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MAIN_dGV_tax.HeaderText = "IVA";
            this.MAIN_dGV_tax.Name = "MAIN_dGV_tax";
            this.MAIN_dGV_tax.ReadOnly = true;
            this.MAIN_dGV_tax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAIN_dGV_tax.Width = 30;
            // 
            // SalesRegisterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "SalesRegisterControl";
            this.Size = new System.Drawing.Size(646, 510);
            this.Load += new System.EventHandler(this.SalesRegisterControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.catProductosBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MAIN_dGV)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource catProductosBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label SEARCHPROVIDER_lbl;
        private System.Windows.Forms.Label SAVEPURCHASE_lbl;
        public System.Windows.Forms.Label lblPROVIDER;
        public System.Windows.Forms.DataGridView MAIN_dGV;
        private Button DELETECAMPS_BUTTON;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Panel panel1;
        public TextBox MAIN_txtBox_bonus;
        private Label label11;
        private Label label8;
        private Label label7;
        private Label MAIN_txtbox_GTOTAL;
        public Label MAIN_txtbox_TOTAL;
        public Label MAIN_txtbox_TAX;
        public Label MAIN_txtbox_SUBTOTAL;
        private Label label9;
        private Label label6;
        private Label label4;
        private Label label5;
        public DateTimePicker dateTimePicker1;
        private Label label2;
        public TextBox txtboxPROVEEDOR;
        private TextBox MAIN_BillStatus;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        public TextBox txtboxFOLIO;
        private Label label1;
        private RadioButton radioButton2;
        public TextBox txtBOXREM;
        private RadioButton radioButton1;
        private TextBox txtBoxFACTURA;
        public ComboBox comboBox1;
        private DataGridViewTextBoxColumn MAIN_dGV_code;
        private DataGridViewTextBoxColumn MAIN_dGV_prodname;
        private DataGridViewTextBoxColumn main_dGV_um;
        private DataGridViewTextBoxColumn MAIN_dGV_QT;
        private DataGridViewTextBoxColumn MAIN_dGV_price;
        private DataGridViewTextBoxColumn MAIN_dGV_imprt;
        private DataGridViewTextBoxColumn MAIN_dGV_tax;
    }

}