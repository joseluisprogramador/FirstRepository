using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using Project_Common.Class;
namespace Project_Data.Class
{
    public class CommandSelectInfoUsers
    {
        private readonly SqlConnection? oConnection = new();
        private readonly Connection oSearchConnection = new();
        private SqlCommand? oCommand = null;
        private SqlDataReader? oSqlDataReader = null;
        private string Consult = "";
        public CommandSelectInfoUsers()
        {
            oConnection = oSearchConnection.OpenConnection();
        }

        /*Filtrar la informacion de los estudiantes de acuerdo con el administrador*/
        public List<InfoUsers>? ExecuteInfoStudent(string FullNameAdmin)
        {
            try
            {
                Consult = @$"select p_Code as Codigo,p_User as Usuario,p_TypeUser as [ Tipo Usuario ],p_FullName as [Nombre Completo],p_Email as Email from t_Students 
                         join t_Admins on t_Students.p_IdAdmin=t_Admins.p_IdAdmin 
                         join t_Users on t_Students.p_IdUser=t_Users.p_IdUser 
                         join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson
                         join t_TypeUsers on t_Users.p_IdTypeUser=t_TypeUsers.p_IdTypeUser 
                         where t_Admins.p_IdAdmin=@p_IdAdmin and p_TypeUser='Estudiante'";

                return ListMember(Consult, FullNameAdmin);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }

        /*Filtrar la informacion de los profesores de acuerdo con el administrador*/
        public List<InfoUsers>? ExecuteInfoTeacher(string FullNameAdmin)
        {
            try
            {
                Consult = @$"select p_Code as Codigo,p_User as Usuario,p_TypeUser as [ Tipo Usuario ],p_FullName as [Nombre Completo],p_Email as Email from t_Teachers 
                         join t_Admins on t_Teachers.p_IdAdmin=t_Admins.p_IdAdmin 
                         join t_Users on t_Teachers.p_IdUser=t_Users.p_IdUser 
                         join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson
                         join t_TypeUsers on t_Users.p_IdTypeUser=t_TypeUsers.p_IdTypeUser 
                         where t_Admins.p_IdAdmin=@p_IdAdmin and p_TypeUser='Profesor'";
                return ListMember(Consult, FullNameAdmin);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }


        public List<string>? ExecuteListAdmin()
        {
            try
            {
                Consult = @"select t_Persons.p_FullName from t_TypeUsers join t_Users on t_TypeUsers.p_IdTypeUser = t_Users.p_IdTypeUser " +
                    "join t_Persons on t_Users.p_IdPerson = t_Persons.p_IdPerson where p_TypeUser='Administrador'";
                return SearchListAdmin(Consult);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
        }

        /*/////////////////////////////////////////////////////////////////////////*/

        /*Obtener la lista de todos los administradores*/
        private List<string>? SearchListAdmin(string Consult)
        {
            try
            {
                List<string> oListAdmin = new();
                oCommand = new(Consult, oConnection);
                oSqlDataReader = oCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    string FullName = oSqlDataReader.GetString(0);
                    oListAdmin.Add(FullName);
                };
                return oListAdmin;
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
            finally
            {
                oSearchConnection.CloseConnection(oConnection);
            }
        }


        /*Obtener el nombre completo del miembro estudiante,profesor o administrador*/
        public (bool, string) SelectFullNameMember(string User)
        {
            try
            {
                oCommand = new("select t_Persons.p_FullName from t_Users join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson where p_User=@p_User", oConnection);
                oCommand.Parameters.AddWithValue("@p_User", User);
                oSqlDataReader = oCommand.ExecuteReader();
                oSqlDataReader.Read();
                string FullName = oSqlDataReader.GetString(0);
                return (true, FullName);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false, "Error en la consulta");
            }
            finally
            {
                oSearchConnection.CloseConnection(oConnection);
            }

        }

        /*Obtener la lista de estudiantes y profesores con su respectivo administrador*/
        private List<InfoUsers>? ListMember(string Consult, string FullNameAdmin)
        {
            try
            {
                InfoUsers oInfoUsers;
                List<InfoUsers> oListUsers = new();
                int? Id = SelectIdAdmin(FullNameAdmin);
                if (Id == null || string.IsNullOrEmpty(Consult)) return null;
                oCommand = new(Consult, oConnection);
                oCommand.Parameters.AddWithValue("@p_IdAdmin", SqlDbType.Int).Value = Id;
                oSqlDataReader = oCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    oInfoUsers = new InfoUsers()
                    {
                        Codigo = Convert.ToString(oSqlDataReader.GetInt32(0)),
                        Usuario = oSqlDataReader.GetString(1),
                        Tipo_Usuario = oSqlDataReader.GetString(2),
                        Nombre_Completo = oSqlDataReader.GetString(3),
                        Email = oSqlDataReader.GetString(4),
                    };
                    oListUsers.Add(oInfoUsers);
                }
                return oListUsers;
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }
            finally
            {
                oSearchConnection.CloseConnection(oConnection);
            }
        }

        private int? SelectIdAdmin(string FullNameAdmin)
        {
            try
            {
                oCommand = new("select t_Admins.p_IdAdmin from t_Users " +
                    "join t_Admins on t_Users.p_IdUser=t_Admins.p_IdUser " +
                    "join t_Persons on t_Users.p_IdPerson=t_Persons.p_IdPerson " +
                    "where t_Persons.p_FullName=@p_FullName", oConnection);
                oCommand.Parameters.AddWithValue("@p_FullName", FullNameAdmin);
                object Id = oCommand.ExecuteScalar();
                return Id != null && Id is int ? Convert.ToInt32(Id) : null;
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null;
            }

        }

    }
}
