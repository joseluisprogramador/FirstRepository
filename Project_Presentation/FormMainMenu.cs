using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Project_Common.Class;
using Project_Domain.Class;
/*using Project_Presentation.Students;*/
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Project_Presentation
{
    public partial class FormMainMenu : Form
    {
        private readonly FormLogin? oFormLogin = new();
        private readonly UserSession? oUserSession = new();
        private FormOptionsMember? oFormOptionsMember = null;
        private string FileImage = "";
        public Panel oPanelContainer
        {
            get { return PanelContainer; }
        }
        public FormMainMenu(FormLogin? oFormLogin = null, UserSession? oUserSession = null)
        {
            InitializeComponent();


            try
            {

                this.oUserSession = oUserSession;
                this.oFormLogin = oFormLogin;
                oFormOptionsMember = new(this.oUserSession);
                PanelOptionAdmin.Visible = false;
                LinkLabelPanelOptionAdmin.LinkClicked += LinkLabelClosePanelOptionAdminLinkCliked;
                LoadUserInformation();
                ApplyDateTime();
                SessionUser();
                /* ComboBoxListAdmin.DropDownStyle = ComboBoxStyle.DropDownList;*/

            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.Message);
            }
        }


        private void CenterPanel()
        {
            int x = (PanelContainer.Width - PanelOptionAdmin.Width) / 2;
            int y = (PanelContainer.Height - PanelOptionAdmin.Height) / 2;
            PanelOptionAdmin.Location = new Point(x, y);

            int x1 = (PanelContainer.Width - PanelSchool.Width) / 2;
            int y1 = (PanelContainer.Height - PanelSchool.Height) / 2;
            PanelSchool.Location = new Point(x1, y1);

        }

        private bool LoadTeacherStatus()
        {

            if (oUserSession != null && oUserSession.RolUser == Rols.RolTeacher && !string.IsNullOrEmpty(oUserSession.User))
            {

                MessageBox.Show(oUserSession.User);
                string FilePath = $"EnabledButtonTeacher{oUserSession.User}.txt";

                if (File.Exists(FilePath))
                {
                    using StreamReader oReader = new(FilePath);
                    string? FileRead = oReader.ReadLine();
                    if (!string.IsNullOrEmpty(FileRead))
                    {
                        return bool.Parse(FileRead);
                    }
                }
            }
            return false;
        }

        private bool LoadStudentStatus()
        {

            if (oUserSession != null && oUserSession.RolUser == Rols.RolStudent && !string.IsNullOrEmpty(oUserSession.User))
            {

                MessageBox.Show(oUserSession.User);
                string FilePath = $"EnabledButtonStudent{oUserSession.User}.txt";
                if (File.Exists(FilePath))
                {
                    using StreamReader oReader = new(FilePath);
                    string? FileRead = oReader.ReadLine();
                    if (!string.IsNullOrEmpty(FileRead))
                    {
                        return bool.Parse(FileRead);
                    }
                }
            }
            return false;
        }
        private void ApplyDateTime()
        {
            LabelDate.Text = "Fecha Actual : " + DateTime.Now.ToString("dd/MM/yyyy");
            TimerApp.Tick += (oSender, oEvent) =>
            {
                LabelTime.Text = "Hora Actual : " + DateTime.Now.ToString("HH:mm:ss");
            };
            TimerApp.Start();
        }


        private void SessionUser()
        {
            if (oUserSession == null) return;

            if (oUserSession.RolUser == Rols.RolTeacher)
                ButtonMember.Text = Rols.RolTeacher;
            if (oUserSession.RolUser == Rols.RolStudent)
                ButtonMember.Text = Rols.RolStudent;
            if (oUserSession.RolUser == Rols.RolAdministrator)
                ButtonMember.Text = Rols.RolAdministrator;
        }

        public void LoadUserInformation()
        {
            if (oUserSession == null) return;
            if (oUserSession.RolUser == Rols.RolStudent)
                EnvironmentStudent();
            if (oUserSession.RolUser == Rols.RolTeacher)
                EnvironmentTeacher();
            if (oUserSession.RolUser == Rols.RolAdministrator)
                EnvironmentAdministrator();
        }

        private void ApplyImageUsers(string File)
        {
            PictureBoxImageGif.Image = Image.FromFile($"C:\\Users\\Jose\\Downloads\\Img\\{File}");
            PictureBoxImageGif.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void InfoUserInMenu(UserSession oUserSession, string File)
        {
            LabelUser.Text = "Usuario : " + oUserSession.User;
            LabelRol.Text = "Rol Usuario : " + oUserSession.RolUser;
            ApplyImageUsers(File);

        }

        delegate void ActionUser(object? oSender, EventArgs oEvent);

        private void EnvironmentStudent()
        {
            if (oUserSession == null) return;
            FileImage = "Alumno1.png";
            InfoUserInMenu(oUserSession, FileImage);
            ActionButtonMember(oUserSession, ButtonMember,
                LoadStudentStatus(), new ActionUser(ButtonClickStudent));
        }

        private void EnvironmentTeacher()
        {
            if (oUserSession == null) return;
            FileImage = "Profesor.png";
            InfoUserInMenu(oUserSession, FileImage);
            ActionButtonMember(oUserSession, ButtonMember,
                LoadTeacherStatus(), new ActionUser(ButtonClickTeachers));
        }
        private void EnvironmentAdministrator()
        {
            if (oUserSession == null) return;
            FileImage = "Administrador.png";
            InfoUserInMenu(oUserSession, FileImage);
            ActionButtonMember(oUserSession, ButtonMember, true, new ActionUser(ButtonClickAdmin));
            CenterPanel();
        }

        private static void ActionButtonMember(UserSession oUserSession, Button oButton, bool IsEnabled, ActionUser oAction)
        {
            oButton.Text = oUserSession.RolUser;
            oButton.Enabled = IsEnabled;
            oButton.Click += new EventHandler(oAction);
        }

        private void EnabledPanelAdministrator(object? oSender, EventArgs oEvent)
        {
            PanelOptionAdmin.Visible = true;
            PanelSchool.Visible = false;
        }


        private void ButtonClickLogout(object? oSender, EventArgs oEvent)
        {
            if (oFormLogin == null) return;
            oFormLogin.Show();
            Close();
        }
        private void ButtonClickStudent(object? oSender, EventArgs oEvent)
        {
            if (oFormOptionsMember == null) return;
            PanelSchool.Visible = false;
            PanelOptionAdmin.Visible = true;
            ButtonMember.Enabled = false;
            ButtonManagementUser.Text = "Gestion De Actualizacion";
            ButtonManagementCourse.Text = "Gestion De Vinculacion";

            ButtonManagementUser.Click += (oSender, oEvent) =>
            {
                oFormOptionsMember.TopLevel = false;
                oFormOptionsMember.Dock = DockStyle.Fill;
                PanelContainer.Controls.Add(oFormOptionsMember);
                TabPageGestionUpdateStudent();
                oFormOptionsMember.Show();
            };
        }

        private void TabPageGestionUpdateStudent()
        {
            if (oFormOptionsMember == null) return;
            TabPage oTabPage = oFormOptionsMember.oTabControl.TabPages[0];
            int Position = oFormOptionsMember.oTabControl.TabPages.IndexOf(oTabPage);
            oFormOptionsMember.oTabControl.SelectedIndex = Position;
        }

        private void ButtonClickTeachers(object? oSender, EventArgs oEvent)
        {
            if (oFormOptionsMember == null) return;
            ButtonManagementUser.Text = "Gestion Actualizacion";
            ButtonManagementCourse.Text = "Gestion De Cursos";
            ButtonSecurityPermissions.Text = "Gestion Vinculacion";
            PanelOptionAdmin.Visible = true;
            PanelSchool.Visible = false;

            ButtonManagementUser.Click += (oSender, oEvent) =>
            {

            };
            ButtonManagementCourse.Click += (oSender, oEvent) =>
            {

            };

            ButtonSecurityPermissions.Click += (oSender, oEvent) =>
            {

            };
        }

        private void ButtonClickAdmin(object? oSender, EventArgs oEvent)
        {
            if (oFormOptionsMember == null) return;
            ButtonManagementUser.Text = "Gestion De Usuario";
            ButtonManagementCourse.Text = "Gestion De Curso";
            PanelOptionAdmin.Visible = true;
            PanelSchool.Visible = false;

            ButtonManagementUser.Click += (oSender, oEvent) =>
            {

            };
            PictureBoxStudent.Visible = true;
        }



        private void LinkLabelClosePanelOptionAdminLinkCliked(object oSender, LinkLabelLinkClickedEventArgs oEvent)
        {
            PanelOptionAdmin.Visible = false;
            PanelSchool.Visible = true;
            ButtonMember.Enabled = true;
        }


        public void ShowPanelImage()
        {
            PanelSchool.Visible = true;
        }
        public void ShowPanelOptionAdmin()
        {
            PanelOptionAdmin.Visible = true;
        }
    }
}

/*
 public void LoadUserInformation()
 {
     if (oFormStudent is not null)
     {
         if (oUserSession.User != null)
         {
             if (oUserSession.RolUser == Rols.RolStudent)
             {

                 string ImagenAlumno = "Alumno1.png";
                 LabelUser.Text = "Usuario : " + oUserSession.User;
                 LabelRol.Text = "Rol Usuario : " + oUserSession.RolUser;
                 ButtonMember.Enabled = false;
                 ButtonMember.Text = "Estudiante";
                 ApplyImageUsers(ImagenAlumno);
                 PanelVinculate.Visible = true;

                 ButtonMember.Click += ButtonClickStudent;
             }

             if (oUserSession.RolUser == Rols.RolTeacher)
             {

                 string ImagenProfesor = "Profesor.png";
                 LabelUser.Text = "Usuario : " + oUserSession.User;
                 LabelRol.Text = "Rol Usuario : " + oUserSession.RolUser;
                 ButtonMember.Enabled = false;
                 ButtonMember.Text = "Profesor";                      
                 ApplyImageUsers(ImagenProfesor);
                 PanelVinculate.Visible = true;

                 EnableControlsToTeacher();
                 ButtonMember.Click += ButtonClickTeachers;
             }

             if (oUserSession.RolUser == Rols.RolAdministrator)
             {

                 string ImageAdministrador = "Administrador.png";
                 LabelUser.Text = "Usuario : " + oUserSession.User;
                 LabelRol.Text = "Rol Usuario : " + oUserSession.RolUser;
                 ButtonMember.Text = "Administrador";
                 ApplyImageUsers(ImageAdministrador);
                 PanelVinculate.Visible = false;

                 CenterPanel();
                 ButtonMember.Click += EnablesPanelAdmin ;
                 ButtonManagementUser.Click += ButtonClickAdmin;
             }
         }
     }
 }
 */


/*
   private void ButtonClickVinculateAdmin(object? oSender, EventArgs oEvent)
        {
            try
            {
                string? Administrator = ComboBoxListAdmin.SelectedItem.ToString();
                if (oUserSession.RolUser == Rols.RolStudent && !string.IsNullOrEmpty(oUserSession.User) && !string.IsNullOrEmpty(Administrator))
                {
                    (bool IsMember, string result) = ModelUser.SelectInfoMember(oUserSession.User);
                    if (IsMember && !string.IsNullOrEmpty(result))
                    {
                        bool IsCheckStudentVinculate = ModelUser.CheckStudentVinculate(result);
                        if (!IsCheckStudentVinculate)
                        {
                            (bool IsVinculate, string Message) =
                             ModelUser.VinculateStudentUser(result, Administrator);
                            if (IsVinculate)
                                MessageError(Message);
                            else
                                MessageError(Message);
                        }
                        else MessageError("El estudiante ya esta vinculado a un administrador");
                        
                    }
                    else MessageError(result);
                }
                else if (oUserSession.RolUser == Rols.RolTeacher && !string.IsNullOrEmpty(oUserSession.User) && !string.IsNullOrEmpty(Administrator))
                {
                    (bool IsMember, string result) = ModelUser.SelectInfoMember(oUserSession.User);
                    if (IsMember && !string.IsNullOrEmpty(result))
                    {
                        bool IsCheckTeacherVinculate = ModelUser.CheckTeacherVinculate(result);
                        if (!IsCheckTeacherVinculate)
                        {
                            (bool IsVinculate, string Message) =
                            ModelUser.VinculateTeacherUser(result, Administrator);
                            if (IsVinculate)
                                MessageError(Message);
                            else
                                MessageError(Message);
                        }
                        else  MessageError("El profesor ya esta vinculado a un administrador");
                    }
                    else MessageError(result);
                }
                else MessageError("Es posible que no haya administradores registrados o que no haya seleccionado ninguno");
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }
        }

 */

/*
          oControl1 = this.TableLayoutPanelMenu.GetControlFromPosition(0, 0);
         oControl2 = this.TableLayoutPanelMenu.GetControlFromPosition(1, 0);
         private void RolsEstudent(Control oControl1, Control oControl2, int Index)
         {
             TableLayoutPanelMenu.Controls.Remove(oControl1);
             TableLayoutPanelMenu.Controls.Remove(oControl2);
             TableLayoutPanelMenu.RowStyles.RemoveAt(Index);
             TableLayoutPanelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
             TableLayoutPanelMenu.AutoSize = true;
             TableLayoutPanelMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
             TableLayoutPanelMenu.Dock = DockStyle.Fill;
         }
         private void RolsTeacher(Control oControl1, Control oControl2, int Index)
         {
             TableLayoutPanelMenu.Controls.Add(oControl1, 0, Index);
             TableLayoutPanelMenu.Controls.Add(oControl2, 1, Index);
             RowStyle newRowStyle = new RowStyle();
             TableLayoutPanelMenu.RowStyles.Insert(Index, newRowStyle);
         }
*/
/*
private void EnvironmentStudent(UserSession oUserSession)
{
    FileImage = "Alumno1.png";
    InfoUserInMenu(oUserSession, FileImage);

    ButtonMember.Text = oUserSession.RolUser;
    ButtonMember.Enabled = LoadStudentStatus();
    ButtonMember.Click += ButtonClickStudent;
    ButtonConfig.Click += ButtonClickStudent;
}

private void EnvironmentTeacher(UserSession oUserSession)
{
    FileImage = "Profesor.png";
    InfoUserInMenu(oUserSession, FileImage);
    ButtonMember.Text = oUserSession.RolUser;
    ButtonMember.Enabled = LoadTeacherStatus();
    ButtonMember.Click += ButtonClickTeachers;

    ButtonConfig.Click += ButtonClickStudent;
}

private void EnvironmentAdministrator(UserSession oUserSession)
{
    FileImage = "Administrador.png";
    InfoUserInMenu(oUserSession, FileImage);
    ButtonMember.Text = oUserSession.RolUser;
    ButtonMember.Enabled = true;
    ButtonMember.Click += EnabledPanelAdministrator;
    ButtonManagementUser.Click += ButtonClickAdmin;
    CenterPanel();
}
*/