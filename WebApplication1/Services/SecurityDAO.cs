using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SecurityDAO
    {
        public SecurityDAO()
        {
        }

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public bool findUser(User user)
        {

            bool result = false;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE Email = @email AND password = @password";

            using (SqlConnection connnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connnection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 60).Value = user.EMail;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 60).Value = user.password;

                try
                {
                    connnection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            sqlStatement = "SELECT * FROM dbo.ClubAccounts WHERE Email = @email AND password = @password";

            using (SqlConnection connnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connnection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 60).Value = user.EMail;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 60).Value = user.password;

                try
                {
                    connnection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return result;
        }
    }
}
