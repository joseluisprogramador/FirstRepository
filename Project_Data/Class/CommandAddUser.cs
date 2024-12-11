using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Data.Common;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Identity.Client;
using System.Diagnostics.Metrics;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Http.Headers;
using Project_Common.Class;

namespace Project_Data.Class
{
    public sealed class CommandAddUser
    {
        private readonly Users ? oUsers = null;
        private readonly SqlConnection? oConnection = new();
        private readonly Connection oSearchConnection = new();
        private SqlCommand? oCommand = null;
        private string Consult = "";
        public CommandAddUser(string ? Code = null, string ? User = null, string ? TypeUser = null, string ? FullName = null , string ? Password = null, string ? Email = null)
        {

            oUsers = new()
            {
                Code = Code ,
                User = User ,
                TypeUser = TypeUser,
                FullName = FullName,
                Password = Password,
                Email = Email,
            };
            oConnection = oSearchConnection.OpenConnection();
        }

        public (bool, string) RegisterUser()
        {

            try
            {
                if (oUsers != null && !string.IsNullOrEmpty(oUsers.Code) && !string.IsNullOrEmpty(oUsers.User) &&
                    !string.IsNullOrEmpty(oUsers.TypeUser) && !string.IsNullOrEmpty(oUsers.FullName) &&
                    !string.IsNullOrEmpty(oUsers.Password) && !string.IsNullOrEmpty(oUsers.Email))
                {
                    if (oUsers.TypeUser == Rols.RolTeacher)
                        return ExecuteInsertTeacher(oUsers.Code, oUsers.User, oUsers.TypeUser, oUsers.FullName, oUsers.Password, oUsers.Email);
                    if (oUsers.TypeUser == Rols.RolStudent)
                        return ExecuteInsertStudent(oUsers.Code, oUsers.User, oUsers.TypeUser, oUsers.FullName, oUsers.Password, oUsers.Email);
                    if (oUsers.TypeUser == Rols.RolAdministrator)
                        return ExecuteInsertAdmin(oUsers.Code, oUsers.User, oUsers.TypeUser, oUsers.FullName, oUsers.Password, oUsers.Email);
                }
                                  
                return (false, "Posibles registros nulos");
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error durante el proceso de registro");
            }
        }

        /*////////////////////////////////////////////////////////////////////////////////////////////////*/
        private (bool, string) ExecuteInsertUser(string Code, string User, string TypeUser, string FullName, string Password, string Email)
        {
            try
            {
                int? CountPerson = SelectCountPerson(FullName);

                if (CountPerson == null || CountPerson == 0)
                     InsertPerson(Code, FullName);

                int? IdPerson = SelectIdPerson(FullName);
                int? IdTypeUser = SelectIdTypeUser(TypeUser);

                if (IdTypeUser == null || IdPerson == null)
                     return (false, " El administrador no ha establecido los roles(Estudiante y Profesor");

                  InsertUser(User, Password, Email, (int)IdTypeUser, (int)IdPerson);

                if (TypeUser != Rols.RolAdministrator)
                      return (true, "Se ha registrado el usuario correctamente");

                int? IdUserAdmin = SelectIdPersonForAdmin(FullName);
                if (IdUserAdmin != null)
                      InsertAdmin((int)IdUserAdmin);

                return (true, "Se ha registrado el usuario correctamente");
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error durante el proceso de registro");
            }
        }
        private (bool, string) ExecuteInsertAdmin(string Code, string User, string TypeUser, string FullName, string Password, string Email)
        {
            try
            {
                return ExecuteInsertUser(Code, User, TypeUser, FullName, Password, Email);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error durante el proceso de registro");
            }
        }
        private (bool, string) ExecuteInsertStudent(string Code, string User, string TypeUser, string FullName, string Password, string Email)
        {
            try
            {
                return ExecuteInsertUser(Code, User, TypeUser, FullName, Password, Email);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error durante el proceso de registro");
            }
        }
        private (bool, string) ExecuteInsertTeacher(string Code, string User, string TypeUser, string FullName, string Password, string Email)
        {
            try
            {
                return ExecuteInsertUser(Code, User, TypeUser, FullName, Password, Email);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error durante el proceso de registro");
            }
        }

        /*//////////////////////////////////////////////////////////////////////////////////////////////////////////*/
        private void InsertUser(string User, string Password, string Email, int IdTypeUser, int IdPerson)
        {
            try
            {
                oCommand = new(@"insert into t_Users(p_User, p_Password,p_Email, p_IdTypeUser,p_IdPerson) 
                                        values(@p_User, @p_Password, @p_Email, @p_IdTypeUser,@p_IdPerson)", oConnection);
                oCommand.Parameters.AddWithValue("@p_User", User);
                oCommand.Parameters.AddWithValue("@p_Password", oSearchConnection.EncryptPassword(Password));
                oCommand.Parameters.AddWithValue("@p_Email", Email);
                oCommand.Parameters.AddWithValue("@p_IdTypeUser", IdTypeUser);
                oCommand.Parameters.AddWithValue("@p_IdPerson", IdPerson);
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }
        }
        private void InsertPerson(string Code, string FullName)
        {
            try
            {
                oCommand = new(@"insert into t_Persons(p_Code,p_FullName) 
                                        values(@p_Code, @p_FullName)", oConnection);
                oCommand.Parameters.AddWithValue("@p_Code", Code);
                oCommand.Parameters.AddWithValue("@p_FullName", FullName);
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }
        }
        private void InsertAdmin(int IdUser)
        {
            try
            {
                oCommand = new("insert into t_Admins(p_IdUser) values(@p_IdPerson)", oConnection);
                oCommand.Parameters.AddWithValue("@p_IdPerson", IdUser);
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {
                Console.Write(oException.Message);
            }
        }
        public (bool, string) InsertStudent(string FullNameStudent, string FullNameAdmin)
        {

            try
            {
                Consult = "insert into t_Students(p_IdUser,p_IdAdmin) values(@p_IdUser,@p_IdAdmin)";
                return InsertMember(FullNameStudent, FullNameAdmin, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error en la consulta");
            }
        }

        public (bool, string) InsertTeacher(string FullNameTeacher, string FullNameAdmin)
        {
            try
            {
                Consult = "insert into t_Teachers(p_IdUser,p_IdAdmin) values(@p_IdUser,@p_IdAdmin)";
                return InsertMember(FullNameTeacher, FullNameAdmin, Consult);

            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error en la consulta");
            }
        }

        public bool ExistsStudentBoundToAdmin(string FullNameStudent)
        {
            try
            {
                Consult = "select t_Students.p_IdAdmin from t_Students join t_Users on t_Students.p_IdUser=t_Users.p_IdUser" +
                    " join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson where t_Persons.p_FullName=@p_FullName";
                return ExistsMemberBound(FullNameStudent, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return false;
            }
        }

        public bool ExistsTeacherBoundToAdmin(string FullNameTeacher)
        {
            try
            {
                Consult = "select t_Teachers.p_IdAdmin from t_Teachers join t_Users on t_Teachers.p_IdUser=t_Users.p_IdUser " +
                    "join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson where t_Persons.p_FullName=@p_FullName";
                return ExistsMemberBound(FullNameTeacher, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return false;
            }
        }

        private int? SelectIdTypeUser(string TypeUser)
        {
            try
            {
                Consult = @"select p_IdTypeUser from t_TypeUsers where p_TypeUser=@p_data";
                return SelectId(TypeUser, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }

        }
        private int? SelectCountPerson(string FullName)
        {
            try
            {
                Consult = @"select count(*) from t_Persons where p_FullName=@p_data";
                return SelectId(FullName, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }
        private int? SelectIdPerson(string FullName)
        {
            try
            {
                Consult = @"select p_IdPerson from t_Persons where p_FullName=@p_data";
                return SelectId(FullName, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }

        private int? SelectIdUserMember(string FullNameMember) /*Los miembros son estudiantes o profesores*/
        {
            try
            {
                Consult = "select t_Users.p_IdUser from t_Users join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson where t_Persons.p_FullName=@p_data";
                return SelectId(FullNameMember, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }
        private int? SelectIdPersonForAdmin(string FullNameAdmin) /*Obtener el Id del administrador seleccionado al que estudiante y profesor se van a vincular*/
        {
            try
            {
                Consult = "select t_Users.p_IdUser from t_Users join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson where t_Persons.p_FullName=@p_data";
                return SelectId(FullNameAdmin, Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }

        private int? SelectId(string data, string Consult)
        {
            oCommand = new(Consult, oConnection);
            oCommand.Parameters.Add("@p_data", SqlDbType.Char).Value = data;
            object? Id = oCommand.ExecuteScalar();
            return Id != null && Id is int ? Convert.ToInt32(Id) : null;
        }

        private bool ExistsMemberBound(string Member, string Consult)
        {
            try
            {
                oCommand = new(Consult, oConnection);
                oCommand.Parameters.AddWithValue("@p_Member", Member);
                object? Id = oCommand.ExecuteScalar();
                return Id != null; /*true si existe ya un profesor que se vinculo a un administrador*/
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return false;
            }
        }

        private (bool, string) InsertMember(string FullNameMember, string FullNameAdmin, string Consult)
        {
            try
            {
                oCommand = new(Consult, oConnection);
                int? IdUser = SelectIdUserMember(FullNameMember);
                int? IdAdmin = SelectIdPersonForAdmin(FullNameAdmin);
                if (IdUser == null && IdAdmin == null) return (false, "No se puedo vincular porque el profesor o el administrador no existe");
                oCommand.Parameters.AddWithValue("@p_IdUser", IdUser);
                oCommand.Parameters.AddWithValue("@p_IdAdmin", IdAdmin);
                oCommand.ExecuteNonQuery();
                return (true, "Se vinculo correctamente");
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error en la consulta");
            }

        }

    }
}



