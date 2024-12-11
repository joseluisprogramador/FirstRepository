using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Project_Common.Class
{
    public class Users
    {
        public string? User { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public string? TypeUser { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    public class UserSession
    {
        public int IdUser { get; set; }
        public string? User { get; set; } 
        public string? RolUser { get; set; } 
    }

    public struct Rols
    {
        public const string RolAdministrator = "Administrador";
        public const string RolTeacher = "Profesor";
        public const string RolStudent = "Estudiante";
        public const string RolText = "Solo para estudiantes";
    }
    public class InfoUsers
    {
        public string Usuario { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string Nombre_Completo { get; set; } = "";
        public string Tipo_Usuario { get; set; } = "";
        public string Email { get; set; } = "";
    }

}
