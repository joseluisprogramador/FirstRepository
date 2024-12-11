using Microsoft.Data.SqlClient;
using Project_Common.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data.Class
{
    public class CommandDeleteUser
    {
        private string User { get; set; }
        private SqlConnection? oConnection = new();
        private Connection oSearchConnection = new();
        public CommandDeleteUser(string User)
        {
            this.User = User;
            oConnection = oSearchConnection.OpenConnection();
        }
        private void CommandUse()
        {
            try
            {
                using SqlCommand oCommand = new("Delete from", oConnection);
                oCommand.Parameters.AddWithValue("@p_User", User);
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
            }

        }
    }
}
