
namespace Project_Presentation
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TextBoxPassword = new TextBox();
            TextBoxUser = new TextBox();
            LinklabelPassword = new LinkLabel();
            LabelUser = new Label();
            LabelPassword = new Label();
            ButtonAcces = new Button();
            LinklabelRegister = new LinkLabel();
            label1 = new Label();
            CheckBoxViewPassword = new CheckBox();
            LinklabelClose = new LinkLabel();
            LabelError = new Label();
            SuspendLayout();
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.BackColor = Color.Snow;
            TextBoxPassword.BorderStyle = BorderStyle.None;
            TextBoxPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxPassword.ForeColor = Color.MediumSlateBlue;
            TextBoxPassword.Location = new Point(39, 152);
            TextBoxPassword.Multiline = true;
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.PasswordChar = '*';
            TextBoxPassword.Size = new Size(322, 25);
            TextBoxPassword.TabIndex = 7;
            // 
            // TextBoxUser
            // 
            TextBoxUser.BackColor = Color.Snow;
            TextBoxUser.BorderStyle = BorderStyle.None;
            TextBoxUser.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxUser.ForeColor = Color.MediumSlateBlue;
            TextBoxUser.Location = new Point(39, 85);
            TextBoxUser.Multiline = true;
            TextBoxUser.Name = "TextBoxUser";
            TextBoxUser.Size = new Size(324, 25);
            TextBoxUser.TabIndex = 5;
            // 
            // LinklabelPassword
            // 
            LinklabelPassword.ActiveLinkColor = Color.Snow;
            LinklabelPassword.AutoSize = true;
            LinklabelPassword.BackColor = Color.White;
            LinklabelPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LinklabelPassword.LinkColor = Color.PaleVioletRed;
            LinklabelPassword.Location = new Point(101, 358);
            LinklabelPassword.Name = "LinklabelPassword";
            LinklabelPassword.Size = new Size(221, 17);
            LinklabelPassword.TabIndex = 1;
            LinklabelPassword.TabStop = true;
            LinklabelPassword.Text = "Has Olvidado Tu Contraseña ?";
            // 
            // LabelUser
            // 
            LabelUser.AutoSize = true;
            LabelUser.BackColor = Color.Snow;
            LabelUser.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelUser.ForeColor = Color.PaleVioletRed;
            LabelUser.Location = new Point(39, 60);
            LabelUser.Name = "LabelUser";
            LabelUser.Size = new Size(63, 17);
            LabelUser.TabIndex = 4;
            LabelUser.Text = "Usuario";
            // 
            // LabelPassword
            // 
            LabelPassword.AutoSize = true;
            LabelPassword.BackColor = Color.Snow;
            LabelPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPassword.ForeColor = Color.PaleVioletRed;
            LabelPassword.Location = new Point(39, 123);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new Size(89, 17);
            LabelPassword.TabIndex = 6;
            LabelPassword.Text = "Contraseña";
            // 
            // ButtonAcces
            // 
            ButtonAcces.BackColor = Color.Snow;
            ButtonAcces.FlatAppearance.MouseDownBackColor = Color.LavenderBlush;
            ButtonAcces.FlatAppearance.MouseOverBackColor = Color.Snow;
            ButtonAcces.FlatStyle = FlatStyle.Flat;
            ButtonAcces.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonAcces.ForeColor = Color.PaleVioletRed;
            ButtonAcces.Location = new Point(41, 253);
            ButtonAcces.Name = "ButtonAcces";
            ButtonAcces.Size = new Size(324, 42);
            ButtonAcces.TabIndex = 9;
            ButtonAcces.Text = "Acceder";
            ButtonAcces.UseVisualStyleBackColor = false;
            // 
            // LinklabelRegister
            // 
            LinklabelRegister.ActiveLinkColor = Color.Snow;
            LinklabelRegister.AutoSize = true;
            LinklabelRegister.BackColor = Color.White;
            LinklabelRegister.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LinklabelRegister.LinkColor = Color.PaleVioletRed;
            LinklabelRegister.Location = new Point(136, 320);
            LinklabelRegister.Name = "LinklabelRegister";
            LinklabelRegister.Size = new Size(151, 17);
            LinklabelRegister.TabIndex = 2;
            LinklabelRegister.TabStop = true;
            LinklabelRegister.Text = "Desea Registrarse ?";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Snow;
            label1.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.PaleVioletRed;
            label1.Location = new Point(154, 22);
            label1.Name = "label1";
            label1.Size = new Size(99, 17);
            label1.TabIndex = 3;
            label1.Text = "Inicio Sesion";
            // 
            // CheckBoxViewPassword
            // 
            CheckBoxViewPassword.AutoSize = true;
            CheckBoxViewPassword.BackColor = Color.Snow;
            CheckBoxViewPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CheckBoxViewPassword.ForeColor = Color.PaleVioletRed;
            CheckBoxViewPassword.Location = new Point(228, 226);
            CheckBoxViewPassword.Name = "CheckBoxViewPassword";
            CheckBoxViewPassword.Size = new Size(137, 21);
            CheckBoxViewPassword.TabIndex = 8;
            CheckBoxViewPassword.Text = "Ver Contraseña";
            CheckBoxViewPassword.UseVisualStyleBackColor = false;
            // 
            // LinklabelClose
            // 
            LinklabelClose.ActiveLinkColor = Color.Snow;
            LinklabelClose.AutoSize = true;
            LinklabelClose.BackColor = Color.White;
            LinklabelClose.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LinklabelClose.LinkColor = Color.PaleVioletRed;
            LinklabelClose.Location = new Point(180, 396);
            LinklabelClose.Name = "LinklabelClose";
            LinklabelClose.Size = new Size(47, 17);
            LinklabelClose.TabIndex = 0;
            LinklabelClose.TabStop = true;
            LinklabelClose.Text = "Close";
            // 
            // LabelError
            // 
            LabelError.AutoSize = true;
            LabelError.BackColor = Color.Snow;
            LabelError.Font = new Font("Britannic Bold", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LabelError.ForeColor = Color.Crimson;
            LabelError.Location = new Point(41, 189);
            LabelError.Name = "LabelError";
            LabelError.Size = new Size(0, 15);
            LabelError.TabIndex = 16;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            ClientSize = new Size(409, 435);
            Controls.Add(LabelError);
            Controls.Add(LinklabelClose);
            Controls.Add(CheckBoxViewPassword);
            Controls.Add(label1);
            Controls.Add(LinklabelRegister);
            Controls.Add(LabelPassword);
            Controls.Add(TextBoxPassword);
            Controls.Add(LabelUser);
            Controls.Add(TextBoxUser);
            Controls.Add(ButtonAcces);
            Controls.Add(LinklabelPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormLogin";
            Opacity = 0.9D;
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private TextBox TextBoxPassword;
        private TextBox TextBoxUser;
        private LinkLabel LinklabelPassword;
        private Label LabelUser;
        private Label LabelPassword;
        private Button ButtonAcces;
        private LinkLabel LinklabelRegister;
        private Label label1;
        private CheckBox CheckBoxViewPassword;
        private LinkLabel LinklabelClose;
        private Label LabelError;
    }
}
