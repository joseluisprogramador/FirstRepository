using Project_Common.Class;
using Project_Data.Class;
using Project_Domain.Class;
using Project_Presentation ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Presentation
{
    public partial class FormOptionsMember : Form
    {
        private readonly UserSession? oUserSession = null;
        private readonly FormMainMenu? oFormMainMenu = new();
        private readonly ModelUser oModelUser = new();

        public FormOptionsMember(UserSession? oUserSession = null, FormMainMenu? oFormMainMenu = null)
        {
            InitializeComponent();
            this.oUserSession = oUserSession;
            this.oFormMainMenu = oFormMainMenu;
            this.ButtonVinculateAdmin.Click += ButtonClickVinculateAdmin;
            LoadAdministrator();
        }
        /*Vincular Administrador*/
        public TabControl oTabControl
        {
            get { return TabControlOptionsMember; }
        }
        private void LoadAdministrator()
        {
            foreach (Control oControl in TabControlOptionsMember.TabPages[0].Controls)
            {
                if (oControl is ComboBox oComboBox && oControl.Name == "ComboBoxListAdmin")
                    oComboBox.DataSource = oModelUser.SelectListAdmin();
                break;
            }
        }
        private void ButtonClickVinculateAdmin(object? oSender, EventArgs oEvent)
        {
            try
            {
                string? Administrator = ComboBoxListAdmin.SelectedItem.ToString();
                InforMember(oUserSession, Administrator);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }
        }

        private void InforMember(UserSession? oUserSession, string? Administrator)
        {
            if (oUserSession != null && !string.IsNullOrEmpty(oUserSession.User) && !string.IsNullOrEmpty(Administrator))
            {
                (bool IsMember, string result) = oModelUser.SelectInfoMember(oUserSession.User);
                if (IsMember && !string.IsNullOrEmpty(result))
                {
                    if (oUserSession.RolUser == Rols.RolStudent)
                        VincularStudentToAdmin(result, Administrator);
                    if (oUserSession.RolUser == Rols.RolTeacher)
                        VincularTeacherToAdmin(result, Administrator);
                }
            }
            else MessageError("Es posible que no haya administradores registrados o que no haya seleccionado ninguno");

        }
        private void VincularStudentToAdmin(string result, string Administrator)
        {
            bool IsCheckStudentVinculate = oModelUser.CheckStudentVinculate(result);
            if (!IsCheckStudentVinculate)
            {
                (bool IsVinculate, string Message) =
                 oModelUser.VincularStudentUser(result, Administrator);
                if (IsVinculate)
                    MessageError(Message);
                else
                    MessageError(Message);
            }
            else MessageError("El estudiante ya esta vinculado a un administrador");
        }
        private void VincularTeacherToAdmin(string result, string Administrator)
        {
            bool IsCheckTeacherVinculate = oModelUser.CheckTeacherVinculate(result);
            if (!IsCheckTeacherVinculate)
            {
                (bool IsVinculate, string Message) =
                oModelUser.VincularTeacherUser(result, Administrator);
                if (IsVinculate)
                    MessageError(Message);
                else
                    MessageError(Message);
            }
            else MessageError("El profesor ya esta vinculado a un administrador");
        }
        private async void MessageError(string message)
        {
            LabelMessage.Text = message;
            LabelMessage.Visible = true;
            await Task.Delay(5000);
            LabelMessage.Visible = false;
        }

        private void CheckBoxChekedChange(object? sender, EventArgs e)
        {
            if (oUserSession != null && oUserSession.RolUser == Rols.RolAdministrator) return;
            CheckBoxEnabled.CheckedChanged += (oSender, oEvent) =>
            {
                if (CheckBoxEnabled.Checked)
                    ComboBoxListAdmin.Enabled = true;
                ComboBoxListDegree.Enabled = true;
                ButtonVinculateAdmin.Enabled = true;

                if (!CheckBoxEnabled.Checked)
                    ComboBoxListAdmin.Enabled = false;
                ComboBoxListDegree.Enabled = false;
                ButtonVinculateAdmin.Enabled = false;
                if (ComboBoxListAdmin.DataSource != null)
                    ComboBoxListAdmin.DataSource = null;
                ComboBoxListAdmin.Items.Clear();
            };

        }

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        /*Actualizar Informacion de Estudiantes,Profesores y Administradores*/
        private void LinkLabelClosePanelOptionAdminLinkCliked(object oSender, LinkLabelLinkClickedEventArgs oEvent)
        {
            if (oFormMainMenu == null) return;
            this.Close();
            oFormMainMenu.ShowPanelOptionAdmin();
        }

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        /*Habilitar Seccion de usuario*/

    }
}


/*
   private void FormVincularMemberForAdmin_Load(object? sender, EventArgs e)
        {
            RedrawRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RedrawRegion(); // Redibujar la región al cambiar el tamaño del formulario
        }

        private void RedrawRegion()
        {
            try
            {
                // Escala de resolución
                int resolutionScale = 5; // Escala ajustada a un valor moderado para buen rendimiento y calidad
                int baseRadius = 10; // Base del radio (en píxeles)

                // Ajustar el radio proporcional al tamaño del formulario
                int radius = baseRadius * resolutionScale;
                int width = this.Width;
                int height = this.Height;

                // Crear un rectángulo con bordes redondeados
                GraphicsPath path = new GraphicsPath();
                path.StartFigure();
                path.AddArc(0, 0, radius, radius, 180, 90); // Esquina superior izquierda
                path.AddArc(width - radius, 0, radius, radius, 270, 90); // Esquina superior derecha
                path.AddArc(width - radius, height - radius, radius, radius, 0, 90); // Esquina inferior derecha
                path.AddArc(0, height - radius, radius, radius, 90, 90); // Esquina inferior izquierda
                path.CloseFigure();

                // Asignar la región al formulario
                this.Region = new Region(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al redibujar la región: " + ex.Message);
            }
        }
 */