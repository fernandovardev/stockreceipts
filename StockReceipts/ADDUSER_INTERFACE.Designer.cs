namespace StockReceipts
{
    partial class UserConfigControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ADDUSER_USERNAME = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DEACTIVATE_BUTTON = new System.Windows.Forms.Button();
            this.ACTIVATE_BUTTON = new System.Windows.Forms.Button();
            this.SAVECHANGES_BUTTON = new System.Windows.Forms.Button();
            this.REGISTER_BUTTON = new System.Windows.Forms.Button();
            this.ADDUSER_STATUS = new System.Windows.Forms.TextBox();
            this.ADDUSER_PASS = new System.Windows.Forms.TextBox();
            this.ADDUSER_LASTNAME2 = new System.Windows.Forms.TextBox();
            this.ADDUSER_LASTNAME1 = new System.Windows.Forms.TextBox();
            this.ADDUSER_NAME = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CONFIG_CHECKLISTBOX = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SALES_CHECKLISTBOX = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userComboBox = new System.Windows.Forms.ComboBox();
            this.ADDUSER_CONFIRMPASS = new System.Windows.Forms.TextBox();
            this.ADDUSER_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ADDUSER_INTERFACE = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.ADDUSER_INTERFACE.SuspendLayout();
            this.SuspendLayout();
            // 
            // ADDUSER_USERNAME
            // 
            this.ADDUSER_USERNAME.Location = new System.Drawing.Point(155, 41);
            this.ADDUSER_USERNAME.MaxLength = 15;
            this.ADDUSER_USERNAME.Name = "ADDUSER_USERNAME";
            this.ADDUSER_USERNAME.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_USERNAME.TabIndex = 0;
            this.ADDUSER_USERNAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.CausesValidation = false;
            this.label7.Location = new System.Drawing.Point(7, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Compras";
            // 
            // DEACTIVATE_BUTTON
            // 
            this.DEACTIVATE_BUTTON.Location = new System.Drawing.Point(459, 45);
            this.DEACTIVATE_BUTTON.Name = "DEACTIVATE_BUTTON";
            this.DEACTIVATE_BUTTON.Size = new System.Drawing.Size(104, 60);
            this.DEACTIVATE_BUTTON.TabIndex = 10;
            this.DEACTIVATE_BUTTON.Text = "Desactivar";
            this.DEACTIVATE_BUTTON.UseVisualStyleBackColor = true;
            this.DEACTIVATE_BUTTON.Visible = false;
            this.DEACTIVATE_BUTTON.Click += new System.EventHandler(this.DEACTIVATE_BUTTON_Click);
            // 
            // ACTIVATE_BUTTON
            // 
            this.ACTIVATE_BUTTON.Location = new System.Drawing.Point(459, 45);
            this.ACTIVATE_BUTTON.Name = "ACTIVATE_BUTTON";
            this.ACTIVATE_BUTTON.Size = new System.Drawing.Size(104, 60);
            this.ACTIVATE_BUTTON.TabIndex = 9;
            this.ACTIVATE_BUTTON.Text = "Activar";
            this.ACTIVATE_BUTTON.UseVisualStyleBackColor = true;
            this.ACTIVATE_BUTTON.Visible = false;
            this.ACTIVATE_BUTTON.Click += new System.EventHandler(this.ACTIVATE_BUTTON_Click);
            // 
            // SAVECHANGES_BUTTON
            // 
            this.SAVECHANGES_BUTTON.Location = new System.Drawing.Point(349, 45);
            this.SAVECHANGES_BUTTON.Name = "SAVECHANGES_BUTTON";
            this.SAVECHANGES_BUTTON.Size = new System.Drawing.Size(104, 60);
            this.SAVECHANGES_BUTTON.TabIndex = 8;
            this.SAVECHANGES_BUTTON.Text = "Guardar Cambios";
            this.SAVECHANGES_BUTTON.UseVisualStyleBackColor = true;
            this.SAVECHANGES_BUTTON.Visible = false;
            this.SAVECHANGES_BUTTON.Click += new System.EventHandler(this.SAVECHANGES_BUTTON_Click);
            // 
            // REGISTER_BUTTON
            // 
            this.REGISTER_BUTTON.Location = new System.Drawing.Point(349, 45);
            this.REGISTER_BUTTON.Name = "REGISTER_BUTTON";
            this.REGISTER_BUTTON.Size = new System.Drawing.Size(104, 60);
            this.REGISTER_BUTTON.TabIndex = 6;
            this.REGISTER_BUTTON.Text = "Registrar";
            this.REGISTER_BUTTON.UseVisualStyleBackColor = true;
            this.REGISTER_BUTTON.Click += new System.EventHandler(this.REGISTER_BUTTON_Click);
            // 
            // ADDUSER_STATUS
            // 
            this.ADDUSER_STATUS.Location = new System.Drawing.Point(403, 126);
            this.ADDUSER_STATUS.MaxLength = 5;
            this.ADDUSER_STATUS.Name = "ADDUSER_STATUS";
            this.ADDUSER_STATUS.ReadOnly = true;
            this.ADDUSER_STATUS.Size = new System.Drawing.Size(130, 20);
            this.ADDUSER_STATUS.TabIndex = 12;
            // 
            // ADDUSER_PASS
            // 
            this.ADDUSER_PASS.Location = new System.Drawing.Point(155, 61);
            this.ADDUSER_PASS.MaxLength = 15;
            this.ADDUSER_PASS.Name = "ADDUSER_PASS";
            this.ADDUSER_PASS.PasswordChar = '*';
            this.ADDUSER_PASS.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_PASS.TabIndex = 1;
            this.ADDUSER_PASS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            // 
            // ADDUSER_LASTNAME2
            // 
            this.ADDUSER_LASTNAME2.Location = new System.Drawing.Point(155, 141);
            this.ADDUSER_LASTNAME2.MaxLength = 255;
            this.ADDUSER_LASTNAME2.Name = "ADDUSER_LASTNAME2";
            this.ADDUSER_LASTNAME2.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_LASTNAME2.TabIndex = 5;
            this.ADDUSER_LASTNAME2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.ADDUSER_LASTNAME2.Leave += new System.EventHandler(this.ADDUSER_LASTNAME2_Leave);
            // 
            // ADDUSER_LASTNAME1
            // 
            this.ADDUSER_LASTNAME1.Location = new System.Drawing.Point(155, 121);
            this.ADDUSER_LASTNAME1.MaxLength = 255;
            this.ADDUSER_LASTNAME1.Name = "ADDUSER_LASTNAME1";
            this.ADDUSER_LASTNAME1.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_LASTNAME1.TabIndex = 4;
            this.ADDUSER_LASTNAME1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.ADDUSER_LASTNAME1.Leave += new System.EventHandler(this.ADDUSER_LASTNAME1_Leave);
            // 
            // ADDUSER_NAME
            // 
            this.ADDUSER_NAME.Location = new System.Drawing.Point(155, 101);
            this.ADDUSER_NAME.MaxLength = 255;
            this.ADDUSER_NAME.Name = "ADDUSER_NAME";
            this.ADDUSER_NAME.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_NAME.TabIndex = 3;
            this.ADDUSER_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.ADDUSER_NAME.Leave += new System.EventHandler(this.ADDUSER_NAME_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "APELLIDO PATERNO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "APELLIDO MATERNO";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.CausesValidation = false;
            this.label9.Location = new System.Drawing.Point(346, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "ESTADO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.CausesValidation = false;
            this.label6.Location = new System.Drawing.Point(3, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "CONFIRMAR CONTRASEÑA";
            // 
            // CONFIG_CHECKLISTBOX
            // 
            this.CONFIG_CHECKLISTBOX.FormattingEnabled = true;
            this.CONFIG_CHECKLISTBOX.Items.AddRange(new object[] {
            "Configuración de compras",
            "Configuración de usuarios"});
            this.CONFIG_CHECKLISTBOX.Location = new System.Drawing.Point(10, 30);
            this.CONFIG_CHECKLISTBOX.Name = "CONFIG_CHECKLISTBOX";
            this.CONFIG_CHECKLISTBOX.Size = new System.Drawing.Size(164, 34);
            this.CONFIG_CHECKLISTBOX.Sorted = true;
            this.CONFIG_CHECKLISTBOX.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.CausesValidation = false;
            this.label8.Location = new System.Drawing.Point(7, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Configuración";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.CausesValidation = false;
            this.label5.Location = new System.Drawing.Point(70, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "CONTRASEÑA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.CausesValidation = false;
            this.label4.Location = new System.Drawing.Point(27, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "NOMBRE DE USUARIO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NOMBRE";
            // 
            // SALES_CHECKLISTBOX
            // 
            this.SALES_CHECKLISTBOX.FormattingEnabled = true;
            this.SALES_CHECKLISTBOX.Items.AddRange(new object[] {
            "Cancelar Compra",
            "Registrar Compra",
            "Reimprimir Ticket"});
            this.SALES_CHECKLISTBOX.Location = new System.Drawing.Point(10, 92);
            this.SALES_CHECKLISTBOX.Name = "SALES_CHECKLISTBOX";
            this.SALES_CHECKLISTBOX.Size = new System.Drawing.Size(164, 49);
            this.SALES_CHECKLISTBOX.Sorted = true;
            this.SALES_CHECKLISTBOX.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userComboBox);
            this.groupBox1.Controls.Add(this.DEACTIVATE_BUTTON);
            this.groupBox1.Controls.Add(this.ACTIVATE_BUTTON);
            this.groupBox1.Controls.Add(this.SAVECHANGES_BUTTON);
            this.groupBox1.Controls.Add(this.REGISTER_BUTTON);
            this.groupBox1.Controls.Add(this.ADDUSER_STATUS);
            this.groupBox1.Controls.Add(this.ADDUSER_CONFIRMPASS);
            this.groupBox1.Controls.Add(this.ADDUSER_PASS);
            this.groupBox1.Controls.Add(this.ADDUSER_ID);
            this.groupBox1.Controls.Add(this.ADDUSER_USERNAME);
            this.groupBox1.Controls.Add(this.ADDUSER_LASTNAME2);
            this.groupBox1.Controls.Add(this.ADDUSER_LASTNAME1);
            this.groupBox1.Controls.Add(this.ADDUSER_NAME);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE USUARIO";
            // 
            // userComboBox
            // 
            this.userComboBox.FormattingEnabled = true;
            this.userComboBox.Location = new System.Drawing.Point(155, 40);
            this.userComboBox.Name = "userComboBox";
            this.userComboBox.Size = new System.Drawing.Size(154, 21);
            this.userComboBox.TabIndex = 14;
            this.userComboBox.Visible = false;
            this.userComboBox.SelectedIndexChanged += new System.EventHandler(this.UserComboBox_SelectedIndexChanged);
            this.userComboBox.Leave += new System.EventHandler(this.UserComboBox_Leave);
            // 
            // ADDUSER_CONFIRMPASS
            // 
            this.ADDUSER_CONFIRMPASS.Location = new System.Drawing.Point(155, 81);
            this.ADDUSER_CONFIRMPASS.MaxLength = 15;
            this.ADDUSER_CONFIRMPASS.Name = "ADDUSER_CONFIRMPASS";
            this.ADDUSER_CONFIRMPASS.PasswordChar = '*';
            this.ADDUSER_CONFIRMPASS.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_CONFIRMPASS.TabIndex = 2;
            this.ADDUSER_CONFIRMPASS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            // 
            // ADDUSER_ID
            // 
            this.ADDUSER_ID.Location = new System.Drawing.Point(155, 20);
            this.ADDUSER_ID.MaxLength = 5;
            this.ADDUSER_ID.Name = "ADDUSER_ID";
            this.ADDUSER_ID.Size = new System.Drawing.Size(154, 20);
            this.ADDUSER_ID.TabIndex = 0;
            this.ADDUSER_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ADDUSER_ID.TextChanged += new System.EventHandler(this.ADDUSER_ID_TextChanged);
            this.ADDUSER_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.ADDUSER_ID.Leave += new System.EventHandler(this.ADDUSER_ID_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.CausesValidation = false;
            this.label10.Location = new System.Drawing.Point(133, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "ID";
            // 
            // ADDUSER_INTERFACE
            // 
            this.ADDUSER_INTERFACE.Controls.Add(this.SALES_CHECKLISTBOX);
            this.ADDUSER_INTERFACE.Controls.Add(this.CONFIG_CHECKLISTBOX);
            this.ADDUSER_INTERFACE.Controls.Add(this.label8);
            this.ADDUSER_INTERFACE.Controls.Add(this.label7);
            this.ADDUSER_INTERFACE.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ADDUSER_INTERFACE.Location = new System.Drawing.Point(0, 188);
            this.ADDUSER_INTERFACE.Name = "ADDUSER_INTERFACE";
            this.ADDUSER_INTERFACE.Size = new System.Drawing.Size(646, 322);
            this.ADDUSER_INTERFACE.TabIndex = 7;
            this.ADDUSER_INTERFACE.TabStop = false;
            this.ADDUSER_INTERFACE.Text = "PERMISOS";
            // 
            // UserConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ADDUSER_INTERFACE);
            this.Name = "UserConfigControl";
            this.Size = new System.Drawing.Size(646, 510);
            this.Load += new System.EventHandler(this.UserConfigControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ADDUSER_INTERFACE.ResumeLayout(false);
            this.ADDUSER_INTERFACE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ADDUSER_USERNAME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button DEACTIVATE_BUTTON;
        private System.Windows.Forms.Button ACTIVATE_BUTTON;
        private System.Windows.Forms.Button SAVECHANGES_BUTTON;
        private System.Windows.Forms.Button REGISTER_BUTTON;
        private System.Windows.Forms.TextBox ADDUSER_STATUS;
        private System.Windows.Forms.TextBox ADDUSER_PASS;
        private System.Windows.Forms.TextBox ADDUSER_LASTNAME2;
        private System.Windows.Forms.TextBox ADDUSER_LASTNAME1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox ADDUSER_INTERFACE;
        public System.Windows.Forms.TextBox ADDUSER_NAME;
        private System.Windows.Forms.TextBox ADDUSER_CONFIRMPASS;
        public System.Windows.Forms.CheckedListBox CONFIG_CHECKLISTBOX;
        public System.Windows.Forms.CheckedListBox SALES_CHECKLISTBOX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ADDUSER_ID;
        private System.Windows.Forms.ComboBox userComboBox;
    }
}
