using System.Globalization;
using System.Windows.Forms;
using Project_Common.Class;
using Project_Domain.Class;

namespace Project_Presentation
{
    public partial class FormLogin : Form
    {
        private FormMainMenu? oMainMenu = null;
        private FormRegisterUser? oRegisterUser = null;
        private FormRecoverPassword? oRecoveredPassword = null;
        private readonly ModelUser oModelUser = new() ;
        private Point LastPoint;
        private string ? User ;
        private string ? Password ;

        public FormLogin()
        {
            InitializeComponent();

            try
            {
                this.ButtonAcces.Click += ButtonClickLogin;

                this.TextBoxUser.TextChanged += TextChangeTextBox;
                this.TextBoxPassword.TextChanged += TextChangeTextBox;

                this.CheckBoxViewPassword.CheckedChanged += CheckedChangeCheckBox;

                this.LinklabelRegister.LinkClicked += LinklabelLinkClicked;
                this.LinklabelPassword.LinkClicked += LinklabelLinkClicked;
                this.LinklabelClose.LinkClicked += LinklabelLinkClicked;

                this.TextBoxUser.KeyPress += KeyPressTextBox;
                this.TextBoxPassword.KeyPress += KeyPressTextBox;

                this.Paint += TextBoxPaint;
            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.Message);
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
            if (oTextBox == null) return ;
            if (oEvent.KeyChar == (char)Keys.Space) oEvent.Handled = true;        
        }
        private void TextChangeTextBox(object? oSender, EventArgs oEvent)
        {
            if (oSender is not TextBox oTextBox) return ;
            int CursorPosition = oTextBox.SelectionStart;
            oTextBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oTextBox.Text.ToLower());
            oTextBox.SelectionStart = CursorPosition;
            LabelError.Visible = false;         
        }
        private void CheckedChangeCheckBox(object? oSender, EventArgs oEvent)
        {
            if (CheckBoxViewPassword.Checked)
                TextBoxPassword.PasswordChar = '\0';
            else
                TextBoxPassword.PasswordChar = '*';
        }
        private void ButtonClickLogin(object? oSender, EventArgs oEvent)
        {
            User = TextBoxUser.Text;
            Password = TextBoxPassword.Text;
            (bool verified, string Result, UserSession? oUserSession) = oModelUser.VerificationUser(User, Password);
            if (!verified && oUserSession == null)
                MessageError(Result);
            else if (verified && oUserSession != null)
                OpenMainMenu(oUserSession, Result);
        }
        private void OpenMainMenu(UserSession oUserSession,string Result)
        {
            MessageError(Result);
            oMainMenu = new(this,oUserSession);
            oMainMenu.Show();
            this.Hide();
        }
        private void MessageError(string message)
        {
            string ImagePathMessageError = @"C:\Users\Jose\Downloads\Img\MensajeError.png";

            // Cargar la imagen
            Image MessageErrorImage = Image.FromFile(ImagePathMessageError);
            Bitmap ResizedMessageErrorImage = new(MessageErrorImage, new Size(24, 24)); // Cambia el tamaño según tus necesidades
            LabelError.Image = ResizedMessageErrorImage;
            LabelError.ImageAlign = ContentAlignment.MiddleLeft; // Para alinear la imagen a la izquierda
            LabelError.TextAlign = ContentAlignment.MiddleRight; // Para alinear el texto a la derecha
            LabelError.Text = $"       {message}";
            LabelError.Visible = true;
            ClearControls();
        }
        private void ClearControls()
        {
            foreach (Control oControl in this.Controls)
            {
                if (oControl is TextBox oTextBox) oTextBox.Text = "";
                else if (oControl is ComboBox oComboBox) oComboBox.Text = "";
            }
        }

        private void LinklabelLinkClicked(object? oSender, LinkLabelLinkClickedEventArgs oEvent)
        {
            LinkLabel? oLinkLabel = oSender as LinkLabel;
            if (oLinkLabel != null)
            {
                if (oLinkLabel.Name is "LinklabelRegister")
                {
                    oRegisterUser = new();
                    oRegisterUser.ShowDialog();
                }
                else if (oLinkLabel.Name is "LinklabelPassword")
                {
                    oRecoveredPassword = new();
                    oRecoveredPassword.ShowDialog();
                }
                else if (oLinkLabel.Name is "LinklabelClose")
                {
                    this.Close();
                }

            }

        }
    }

}



/*
  /* private void Roles(FormMainMenu oMainMenu)
        {
            Control control1 = oMainMenu._TableLayoutPanelMenu.GetControlFromPosition(0, 0);
            Control control2 = oMainMenu._TableLayoutPanelMenu.GetControlFromPosition(1, 0);

            int index = 0; // Índice de la fila que se va a eliminar

            if (ModelUser.VerificationTypeUser is not null)
            {
                if (Rols.RolStudent.Equals("Estudiante"))
                {
                    oMainMenu._TableLayoutPanelMenu.Controls.Remove(control1);
                    oMainMenu._TableLayoutPanelMenu.Controls.Remove(control2);
                    oMainMenu._TableLayoutPanelMenu.RowStyles.RemoveAt(index);
                    oMainMenu._TableLayoutPanelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    oMainMenu._TableLayoutPanelMenu.AutoSize = true;
                    oMainMenu._TableLayoutPanelMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    oMainMenu._TableLayoutPanelMenu.Dock = DockStyle.Fill;
                }
                else if (Rols.RolTeacher.Equals("Profesor"))
                {
                    oMainMenu._TableLayoutPanelMenu.Controls.Add(control1, 0, index);
                    oMainMenu._TableLayoutPanelMenu.Controls.Add(control2, 1, index);
                    RowStyle newRowStyle = new RowStyle();
                    oMainMenu._TableLayoutPanelMenu.RowStyles.Insert(index, newRowStyle);
                }
            }

        }*/
