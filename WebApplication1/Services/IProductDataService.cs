using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductDataService
    {
        List<Product> GetAllProducts();

        List<Product> SearchProduct(string searchTerm);

        Product GetProductById(int productId);

        int Insert(Product product);
        int Update(Product product);
        int Delete(Product product);
        int DeleteAll();


    }
}
