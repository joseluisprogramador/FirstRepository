using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Project_Common.Class;

namespace Project_Data.Class
{
    public class CommandSelectUser
    {
        private readonly Users oUsers;
        private UserSession? oUserSession = null;
        private readonly SqlConnection? oConnection = new();
        private SqlDataReader? oSqlDataReader = null;
        private readonly Connection oSearchConnection = new();
        private string Consult = "";
        public CommandSelectUser(string ? User = null, string ? Password = null)
        {
            oUsers = new()
            {
                User = User,
                Password = Password,
            };
            oConnection = oSearchConnection.OpenConnection();
        }

        public (bool,string,UserSession?) LoginUser()
        {

            try
            {
                Consult = @"select p_IdUser,p_User,p_TypeUser from 
                         t_TypeUsers join t_Users on 
                         t_TypeUsers.p_IdTypeUser=t_Users.p_IdTypeUser 
                         where p_User=@p_User and p_Password=@p_Password";
                if (!string.IsNullOrEmpty(oUsers.User) && !string.IsNullOrEmpty(oUsers.Password))
                {
                    using SqlCommand oCommand = new(Consult, oConnection);
                    oCommand.Parameters.Add("@p_User", SqlDbType.VarChar, 50).Value = oUsers.User;
                    oCommand.Parameters.Add("@p_Password", SqlDbType.VarChar, 64).Value = oSearchConnection.EncryptPassword(oUsers.Password);
                    oSqlDataReader = oCommand.ExecuteReader();
                    if (!oSqlDataReader.HasRows) return (false, "El Usuario no existe,,debe registrarse", null);
                    while (oSqlDataReader.Read())
                    {
                        oUserSession = new()
                        {
                            IdUser = oSqlDataReader.GetInt32(0),
                            User = oSqlDataReader.GetString(1),
                            RolUser = oSqlDataReader.GetString(2)
                        };
                    }
                }
                else return (false, MessageWarning(oUsers.User,oUsers.Password), null);
                return (true, "Inicio Sesion Exitoso", oUserSession);
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return (false,"Error en la consulta" ,null);
            }

        }

        private static string MessageWarning(string ? User, string ? Password)
        {
            if (User == null && Password != null) return "El usuario esta vacio, ¡ Ingreselo !" ;
            if (User != null && Password == null) return "La constraseña esta vacia, ¡ Ingresela !";
            return "Por favor ingrese el usuario y la constraseña";
        }

    }
}
