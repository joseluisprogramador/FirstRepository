using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace Project_Data.Class
{
    public sealed class Connection
    {
        public SqlConnection ? OpenConnection()
        {
            try
            {
                SqlConnection? oConnection =
                    new("Data Source=DESKTOP-K8321M2\\MSSQLSERVER01;Initial Catalog=Db_Company;Integrated Security=True;Trust Server Certificate=True");
                oConnection.Open();
                return oConnection ;
            }
            catch (Exception oException)
            {
                Console.WriteLine(oException.Message);
                return null ;
            }

        }

        public void CloseConnection(SqlConnection ? oConnection)
        {
            if (oConnection != null && oConnection.State == ConnectionState.Open)
                oConnection.Close();
        }

        public string EncryptPassword(string Password)
        {
            SHA256 oSha256 = SHA256.Create();
            byte[] oBytes = oSha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
            StringBuilder oStringBuilder = new();
            foreach (byte Byte in oBytes)
            {
                oStringBuilder.Append(Byte.ToString("x2"));
            }
            return oStringBuilder.ToString();

        }


    }
}
