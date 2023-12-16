using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductDAO : IProductDataService
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        public int Delete(Product product)
        {
            int newIdNumber = -1;

            string sqlStatement = "DELETE FROM dbo.Products WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);

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

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        //private int compareProductsByPrice (Product p1, Product p2) { 
        //    if (p2.Name.CompareTo(p1.Name) > 0)
        //    {
        //        return 0;
        //    }       
        //    return 1;
        //}
        public List<Product> GetAllProducts()
        {
            List <Product> products = new List<Product>();

            string sqlStatement = "SELECT * FROM dbo.Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return products;
        }

        public List<Product> GetAllProductsSorted()
        {
            List<Product> products = new List<Product>();

            string sqlStatement = "SELECT * FROM dbo.Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            //products.Sort(compareProductsByPrice);
            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = new Product();

            string sqlStatement = "SELECT * FROM dbo.Products WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Id", productId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        product = new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return product;
        }

        public int Insert(Product product)
        {
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.Products (Name, Price, Description) VALUES (@Name, @Price, @Description)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@Description", product.Description);

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

        public List<Product> SearchProduct(string searchTerm)
        {
            List<Product> products = new List<Product>();

            string sqlStatement = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');

                try
                {
                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Price = (decimal)reader[2],
                            Description = (string)reader[3]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return products;
        }

        public int Update(Product product)
        {
            int newIdNumber = -1;

            string sqlStatement = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@Description", product.Description);
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);

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
    }
}
