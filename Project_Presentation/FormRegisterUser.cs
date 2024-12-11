using Project_Domain.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_Presentation
{
    public partial class FormRegisterUser : Form
    {
        private Point LastPoint;
        private string User = "";
        private string TypeUser = "";
        private string Code = "";
        private string FullName = "";
        private string Password = "" ;
        private string ConfirmPassword = "" ;
        private string Email = "";
        private readonly ModelUser oModelUser = new();
        public FormRegisterUser()
        {
            InitializeComponent();
            try
            {
                this.ButtonCreateUser.Click += ButtonClickRegisterUser;
                this.TextBoxUser.TextChanged += TextChangeTextBox;
                this.TextBoxPassword.TextChanged += TextChangeTextBox;
                this.TextBoxConfirmPassword.TextChanged += TextChangeTextBox;
                this.TextBoxFullName.TextChanged += TextChangeTextBox;
                this.TextBoxEmail.TextChanged += TextChangeTextBox;
                this.MouseDown += FormMouseDown;
                this.MouseMove += FormMouseMove;
                this.TextBoxCode.Click += TextBoxRandomCodeClick;
                this.LinklabelBack.LinkClicked += LinklabelLinkClicked;
                this.TextBoxUser.KeyPress += KeyPressTextBox;
                this.TextBoxPassword.KeyPress += KeyPressTextBox;
                this.TextBoxConfirmPassword.KeyPress += KeyPressTextBox;
                this.TextBoxEmail.KeyPress += KeyPressTextBox;
                this.Paint += TextBoxPaint;
                LoadTypeUsers();
            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.Message);
            }

        }

        private void LoadTypeUsers()
        {
            ComboBoxTypeUser.Items.Add("Administrador");
            ComboBoxTypeUser.Items.Add("Profesor");
            ComboBoxTypeUser.Items.Add("Estudiante");
        }
        private void TextBoxPaint(object? oSender, PaintEventArgs oEvent)
        {
            Pen? oPen = new(Color.PaleVioletRed, 3);

            foreach (Control oControl in this.Controls)
            {
                if (oControl is TextBox oTextBox)
                {
                    int x1 = oTextBox.Left;
                    int x2 = oTextBox.Right;
                    int y = oTextBox.Bottom + 2;
                    oEvent.Graphics.DrawLine(oPen, x1, y, x2, y);
                }

                else if (oControl is ComboBox oComboBox)
                {
                    int x1 = oComboBox.Left;
                    int x2 = oComboBox.Right;
                    int y = oComboBox.Bottom + 2;
                    oEvent.Graphics.DrawLine(oPen, x1, y, x2, y);
                }
            }
            oPen.Dispose();
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

        /*Que los nombres iniciales de los textbox comienzen en mayuscula*/
        private void TextChangeTextBox(object? oSender, EventArgs oEvent)
        {
            TextBoxBase? oTextBox = oSender as TextBox;
            if (oTextBox is not null)
            {
                int cursorPosition = oTextBox.SelectionStart;
                // Convierte el texto del TextBox a título (primeras letras en mayúsculas)
                oTextBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oTextBox.Text.ToLower());
                // Restaura la posición del cursor
                oTextBox.SelectionStart = cursorPosition;

                if (oTextBox.Name == "TextBoxUser" || oTextBox.Name == "TextBoxPassword" ||
                    oTextBox.Name == "TextBoxConfirmPassword" || oTextBox.Name == "TextBoxFullName" || oTextBox.Name == "TextBoxEmail")
                {
                    if (oTextBox.Text != "")
                    {
                        LabelMessage.Visible = false;
                    }
                }

            }
        }
        /*
        private void ComboBoxTextChange(object? oSender, EventArgs oEvent)
        {
            ComboBox? oComboBox = oSender as ComboBox;
            if (oComboBox is not null)
            {
                if (oComboBox.Text.Equals(Rols.RolTeacher) || oComboBox.Text.Equals(Rols.RolAdministrator))
                {
                    ComboBoxCourse.Text = "Solo para estudiantes";
                    ComboBoxCourse.KeyPress += (oSender, oEvent) =>
                    {
                        oEvent.Handled = true;
                    };
                }
                else
                {
                    ComboBoxCourse.Text = "";
                }
            }

        }
        */
        private void ButtonClickRegisterUser(object? oSender, EventArgs oEvent)
        {

            try
            {
                User = TextBoxUser.Text;
                TypeUser = ComboBoxTypeUser.Text;
                Code = TextBoxCode.Text;
                FullName = TextBoxFullName.Text;
                Password = TextBoxPassword.Text;
                ConfirmPassword = TextBoxConfirmPassword.Text;
                Email = TextBoxEmail.Text;

                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(TypeUser) && !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(FullName) 
                     && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrEmpty(Email))
                {
                    if (Password == ConfirmPassword)
                    {
                        (bool, string) IsRegistered = oModelUser.VerificationRegisterUser(Code, User, TypeUser,FullName,Password,Email);
                        (bool registered, string message) = IsRegistered;

                        if (registered)
                            LabelMessage.Text = message;
                            LabelMessage.Visible = true;
                        if (!registered)
                            LabelMessage.Text = message;
                            LabelMessage.Visible = true;
                    }
                    else MessageError("Contraseña y confirmar contraseña deben ser iguales");
                }
                else MessageError("Faltan datos para poder registrarse");

                ClearControls();
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }
        }

        private void MessageError(string message)
        {
            string ImagePathMessageError = @"C:\Users\Jose\Downloads\Img\MensajeError.png";

            // Cargar la imagen
            Image MessageErrorImage = Image.FromFile(ImagePathMessageError);
            Bitmap ResizedMessageErrorImage = new(MessageErrorImage, new Size(24, 24)); // Cambia el tamaño según tus necesidades
            LabelMessage.Image = ResizedMessageErrorImage;
            LabelMessage.ImageAlign = ContentAlignment.MiddleLeft; // Para alinear la imagen a la izquierda
            LabelMessage.TextAlign = ContentAlignment.MiddleRight; // Para alinear el texto a la derecha
            LabelMessage.Text = $"       {message}";
            LabelMessage.Visible = true;
        }
        private void ClearControls()
        {
            foreach (Control oControl in this.Controls)
            {
                if (oControl is TextBox oTextBox) oTextBox.Text = "";
                else if (oControl is ComboBox oComboBox) oComboBox.Text = "";
            }
        }

        /*Mover y detener formulario*/
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

        private void LinklabelLinkClicked(object? oSender, LinkLabelLinkClickedEventArgs oEvent)
        {
            this.Close();
        }

        private void TextBoxRandomCodeClick(object? oSender, EventArgs oEvent)
        {
            string Code = RandomCode(6);
            TextBoxCode.Text = Code;
        }

        private static string RandomCode(int Longitud)
        {
            const string Caracteres = "0123456789";
            char[] codigo = new char[Longitud];
            Random oRandom = new();
            for (int i = 0; i < Longitud; i++)
            {
                codigo[i] = Caracteres[oRandom.Next(Caracteres.Length)];
            }
            return new string(codigo);
        }
    }
}


/*
      <gcAllowVeryLargeObjects enabled="true" />
      <gcServer enabled="true" />
 */