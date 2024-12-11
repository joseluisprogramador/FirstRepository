
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Presentation
{
    public partial class FormRecoverPassword : Form
    {
        private Point LastPoint;
        public FormRecoverPassword()
        {
            InitializeComponent();

            this.TextBoxEmail.TextChanged += TextChangeTextBox;
            this.TextBoxPassword.TextChanged += TextChangeTextBox;

            this.CheckBoxViewPassword.CheckedChanged += CheckedChangeCheckBox;

            this.TextBoxEmail.KeyPress += KeyPressTextBox;
            this.TextBoxPassword.KeyPress += KeyPressTextBox;

            this.LinklabelBack.LinkClicked += LinklabelLinkClicked;

            this.MouseDown += FormMouseDown;
            this.MouseMove += FormMouseMove;

            this.Paint += TextBoxPaint ;
        }

        private void TextChangeTextBox(object? oSender, EventArgs oEvent)
        {
            TextBoxBase? oTextBox = oSender as TextBox;
            if (oTextBox != null)
            {
                int cursorPosition = oTextBox.SelectionStart;
                // Convierte el texto del TextBox a título (primeras letras en mayúsculas)
                oTextBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oTextBox.Text.ToLower());
                // Restaura la posición del cursor
                oTextBox.SelectionStart = cursorPosition;
            }
        }
        private void KeyPressTextBox(object? oSender, KeyPressEventArgs oEvent)
        {
            TextBoxBase? oTextBox = oSender as TextBox;
            if (oTextBox != null)
            {
                if (oEvent.KeyChar == (char)Keys.Space)
                {
                    oEvent.Handled = true;
                }
            }
        }
        private void TextBoxPaint(object? oSender, PaintEventArgs oEvent)
        {
            Pen oPen = new(Color.PaleVioletRed, 3);
            foreach (Control oControl in this.Controls)
            {
                if (oControl is TextBox oTextBox)
                {
                    int x1 = oTextBox.Left;
                    int x2 = oTextBox.Right;
                    int y = oTextBox.Bottom + 2;
                    oEvent.Graphics.DrawLine(oPen, x1, y, x2, y);
                }
            }
            oPen.Dispose();

        }
        private void LinklabelLinkClicked(object? oSender, LinkLabelLinkClickedEventArgs oEvent)
        {
            this.Close();
        }

        private void CheckedChangeCheckBox(object? oSender, EventArgs oEvent)
        {
            if (CheckBoxViewPassword.Checked)
            {
                TextBoxPassword.PasswordChar = '\0';
            }
            else
            {
                TextBoxPassword.PasswordChar = '*';
            }
        }
        private void FormMouseDown(object? oSender, MouseEventArgs oEvent)
        {
            if (oEvent.Button == MouseButtons.Left)
            {
                LastPoint = new Point(oEvent.X, oEvent.Y);
            }

        }
        private void FormMouseMove(object? oSender, MouseEventArgs oEvent)
        {
            if (oEvent.Button == MouseButtons.Left)
            {
                this.Left += oEvent.X - LastPoint.X;
                this.Top += oEvent.Y - LastPoint.Y;
            }
        }
    }
}



