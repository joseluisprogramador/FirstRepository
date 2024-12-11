namespace Project_Presentation
{
    partial class FormRecoverPassword
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
            CheckBoxViewPassword = new CheckBox();
            label1 = new Label();
            label3 = new Label();
            TextBoxPassword = new TextBox();
            label2 = new Label();
            TextBoxEmail = new TextBox();
            Button_RecoverUser = new Button();
            LinklabelBack = new LinkLabel();
            LabelError = new Label();
            SuspendLayout();
            // 
            // CheckBoxViewPassword
            // 
            CheckBoxViewPassword.AutoSize = true;
            CheckBoxViewPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CheckBoxViewPassword.ForeColor = Color.PaleVioletRed;
            CheckBoxViewPassword.Location = new Point(214, 230);
            CheckBoxViewPassword.Name = "CheckBoxViewPassword";
            CheckBoxViewPassword.Size = new Size(134, 21);
            CheckBoxViewPassword.TabIndex = 26;
            CheckBoxViewPassword.Text = "View Password";
            CheckBoxViewPassword.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Snow;
            label1.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.PaleVioletRed;
            label1.Location = new Point(140, 25);
            label1.Name = "label1";
            label1.Size = new Size(139, 17);
            label1.TabIndex = 25;
            label1.Text = "Recover your user";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Snow;
            label3.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.PaleVioletRed;
            label3.Location = new Point(48, 137);
            label3.Name = "label3";
            label3.Size = new Size(78, 17);
            label3.TabIndex = 23;
            label3.Text = "Password";
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.BackColor = Color.Snow;
            TextBoxPassword.BorderStyle = BorderStyle.None;
            TextBoxPassword.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxPassword.ForeColor = Color.PaleVioletRed;
            TextBoxPassword.Location = new Point(48, 165);
            TextBoxPassword.Multiline = true;
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.PasswordChar = '*';
            TextBoxPassword.Size = new Size(321, 25);
            TextBoxPassword.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Snow;
            label2.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.PaleVioletRed;
            label2.Location = new Point(48, 73);
            label2.Name = "label2";
            label2.Size = new Size(50, 17);
            label2.TabIndex = 22;
            label2.Text = "E-mail";
            // 
            // TextBoxEmail
            // 
            TextBoxEmail.BackColor = Color.Snow;
            TextBoxEmail.BorderStyle = BorderStyle.None;
            TextBoxEmail.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxEmail.ForeColor = Color.PaleVioletRed;
            TextBoxEmail.Location = new Point(48, 101);
            TextBoxEmail.Multiline = true;
            TextBoxEmail.Name = "TextBoxEmail";
            TextBoxEmail.Size = new Size(321, 25);
            TextBoxEmail.TabIndex = 21;
            // 
            // Button_RecoverUser
            // 
            Button_RecoverUser.BackColor = Color.Snow;
            Button_RecoverUser.FlatAppearance.MouseDownBackColor = Color.LavenderBlush;
            Button_RecoverUser.FlatAppearance.MouseOverBackColor = Color.Snow;
            Button_RecoverUser.FlatStyle = FlatStyle.Flat;
            Button_RecoverUser.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Button_RecoverUser.ForeColor = Color.PaleVioletRed;
            Button_RecoverUser.Location = new Point(49, 257);
            Button_RecoverUser.Name = "Button_RecoverUser";
            Button_RecoverUser.Size = new Size(321, 33);
            Button_RecoverUser.TabIndex = 24;
            Button_RecoverUser.Text = "Acces";
            Button_RecoverUser.UseVisualStyleBackColor = false;
            // 
            // LinklabelBack
            // 
            LinklabelBack.AutoSize = true;
            LinklabelBack.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LinklabelBack.ForeColor = Color.PaleVioletRed;
            LinklabelBack.LinkColor = Color.PaleVioletRed;
            LinklabelBack.Location = new Point(185, 306);
            LinklabelBack.Name = "LinklabelBack";
            LinklabelBack.Size = new Size(42, 17);
            LinklabelBack.TabIndex = 28;
            LinklabelBack.TabStop = true;
            LinklabelBack.Text = "Back";
            // 
            // LabelError
            // 
            LabelError.AutoSize = true;
            LabelError.Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LabelError.ForeColor = Color.Crimson;
            LabelError.Location = new Point(49, 204);
            LabelError.Name = "LabelError";
            LabelError.Size = new Size(0, 17);
            LabelError.TabIndex = 29;
            // 
            // FormRecoverPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            ClientSize = new Size(422, 348);
            Controls.Add(LabelError);
            Controls.Add(LinklabelBack);
            Controls.Add(CheckBoxViewPassword);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(TextBoxPassword);
            Controls.Add(label2);
            Controls.Add(TextBoxEmail);
            Controls.Add(Button_RecoverUser);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormRecoverPassword";
            Opacity = 0.9D;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CheckBoxViewPassword;
        private Label label1;
        private Label label3;
        private TextBox TextBoxPassword;
        private Label label2;
        private TextBox TextBoxEmail;
        private Button Button_RecoverUser;
        private LinkLabel LinklabelBack;
        private Label LabelError;
    }
}