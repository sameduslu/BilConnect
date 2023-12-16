using Microsoft.CodeAnalysis;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserDAO : IUserDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        public int Delete(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string sqlStatement = "SELECT * FROM dbo.Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = (int)reader[0],
                            name = (string)reader[1],
                            EMail = (string)reader[2],
                            password = (string)reader[3]
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

        public User GetUserById(int userId)
        {
            User user = new User();

            string sqlStatement = "SELECT * FROM dbo.Users WHERE Id = @Id";

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
                        user = new User
                        {
                            UserId = (int)reader[0],
                            name = (string)reader[1],
                            EMail = (string)reader[2],
                            password = (string)reader[3]
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

        public int Insert(User user)
        {
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.Users (Username, Email, Password) VALUES (@Name, @Email, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", user.name);
                sqlCommand.Parameters.AddWithValue("@Email", user.EMail);
                sqlCommand.Parameters.AddWithValue("@Password", user.password);

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

        public int Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
