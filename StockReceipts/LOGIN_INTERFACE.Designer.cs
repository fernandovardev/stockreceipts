namespace StockReceipts
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label2 = new System.Windows.Forms.Label();
            this.Loginbutton = new System.Windows.Forms.Button();
            this.Loginform_txtbox_user = new System.Windows.Forms.TextBox();
            this.LoginForm_txtbox_pass = new System.Windows.Forms.TextBox();
            this.DB_access = new System.Windows.Forms.Button();
            this.CloseApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(72, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Por favor, introduzca su usuario y contraseña";
            // 
            // Loginbutton
            // 
            this.Loginbutton.BackColor = System.Drawing.Color.LightCoral;
            this.Loginbutton.ForeColor = System.Drawing.Color.Transparent;
            this.Loginbutton.Location = new System.Drawing.Point(63, 254);
            this.Loginbutton.Name = "Loginbutton";
            this.Loginbutton.Size = new System.Drawing.Size(217, 29);
            this.Loginbutton.TabIndex = 3;
            this.Loginbutton.Text = "Aceptar";
            this.Loginbutton.UseVisualStyleBackColor = false;
            this.Loginbutton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Loginform_txtbox_user
            // 
            this.Loginform_txtbox_user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Loginform_txtbox_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loginform_txtbox_user.ForeColor = System.Drawing.Color.Black;
            this.Loginform_txtbox_user.Location = new System.Drawing.Point(97, 129);
            this.Loginform_txtbox_user.MaxLength = 15;
            this.Loginform_txtbox_user.Name = "Loginform_txtbox_user";
            this.Loginform_txtbox_user.Size = new System.Drawing.Size(170, 25);
            this.Loginform_txtbox_user.TabIndex = 1;
            this.Loginform_txtbox_user.Text = "User";
            // 
            // LoginForm_txtbox_pass
            // 
            this.LoginForm_txtbox_pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoginForm_txtbox_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginForm_txtbox_pass.ForeColor = System.Drawing.Color.Black;
            this.LoginForm_txtbox_pass.Location = new System.Drawing.Point(97, 204);
            this.LoginForm_txtbox_pass.MaxLength = 15;
            this.LoginForm_txtbox_pass.Name = "LoginForm_txtbox_pass";
            this.LoginForm_txtbox_pass.PasswordChar = '*';
            this.LoginForm_txtbox_pass.Size = new System.Drawing.Size(170, 25);
            this.LoginForm_txtbox_pass.TabIndex = 2;
            this.LoginForm_txtbox_pass.Text = "Password";
            // 
            // DB_access
            // 
            this.DB_access.BackColor = System.Drawing.Color.LightCyan;
            this.DB_access.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DB_access.ForeColor = System.Drawing.Color.Black;
            this.DB_access.Location = new System.Drawing.Point(626, 333);
            this.DB_access.Name = "DB_access";
            this.DB_access.Size = new System.Drawing.Size(55, 29);
            this.DB_access.TabIndex = 4;
            this.DB_access.Text = "Config.";
            this.DB_access.UseVisualStyleBackColor = false;
            this.DB_access.Click += new System.EventHandler(this.DB_access_Click);
            // 
            // CloseApp
            // 
            this.CloseApp.BackColor = System.Drawing.Color.LightCoral;
            this.CloseApp.ForeColor = System.Drawing.Color.Transparent;
            this.CloseApp.Location = new System.Drawing.Point(63, 289);
            this.CloseApp.Name = "CloseApp";
            this.CloseApp.Size = new System.Drawing.Size(217, 29);
            this.CloseApp.TabIndex = 5;
            this.CloseApp.Text = "Salir";
            this.CloseApp.UseVisualStyleBackColor = false;
            this.CloseApp.Click += new System.EventHandler(this.CloseApp_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(684, 364);
            this.ControlBox = false;
            this.Controls.Add(this.LoginForm_txtbox_pass);
            this.Controls.Add(this.Loginform_txtbox_user);
            this.Controls.Add(this.DB_access);
            this.Controls.Add(this.CloseApp);
            this.Controls.Add(this.Loginbutton);
            this.Controls.Add(this.label2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Loginbutton;
        private System.Windows.Forms.TextBox Loginform_txtbox_user;
        private System.Windows.Forms.TextBox LoginForm_txtbox_pass;
        private System.Windows.Forms.Button DB_access;
        private System.Windows.Forms.Button CloseApp;
    }
}