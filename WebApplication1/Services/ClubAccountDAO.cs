using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ClubAccountDAO : IClubAccountDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        public int Delete(ClubAccount clubAccount)
        {
            throw new NotImplementedException();
        }

        public List<ClubAccount> GetAllClubAccounts()
        {
            List<ClubAccount> users = new List<ClubAccount>();

            string sqlStatement = "SELECT * FROM dbo.ClubAccounts";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new ClubAccount
                        {
                            UserId = (int)reader[0],
                            name = (string)reader[1],
                            EMail = (string)reader[2],
                            password = (string)reader[3],
                            Abbrevation = (string)reader[4]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return users;
        }

        public ClubAccount GetClubAccountById(int userId)
        {
            ClubAccount user = new ClubAccount();

            string sqlStatement = "SELECT * FROM dbo.ClubAccounts WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Id", userId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new ClubAccount
                        {
                            UserId = (int)reader[0],
                            name = (string)reader[1],
                            EMail = (string)reader[2],
                            password = (string)reader[3],
                            Abbrevation = (string)reader[4]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return user;
        }

        public int Insert(ClubAccount clubAccount)
        {
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.ClubAccounts (Username, Email, Password, Abbrevation) VALUES (@Name, @Email, @Password, @Abbrevation)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", clubAccount.name);
                sqlCommand.Parameters.AddWithValue("@Email", clubAccount.EMail); ;
                sqlCommand.Parameters.AddWithValue("@Password", clubAccount.password);
                sqlCommand.Parameters.AddWithValue("@Abbrevation", clubAccount.Abbrevation);

                try
                {
                    connection.Open();

                    newIdNumber = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return newIdNumber;
        }

        public int Update(ClubAccount clubAccount)
        {
            throw new NotImplementedException();
        }
    }
}
