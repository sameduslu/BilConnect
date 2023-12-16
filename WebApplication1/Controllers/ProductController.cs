using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        static ProductDAO productsDAO = new ProductDAO();
        static int productId2;
        public IActionResult Index()
        {
            return View (productsDAO.GetAllProducts());
        }

        public IActionResult SearchResults (string searchTerm) {
            return View ("Index", productsDAO.SearchProduct(searchTerm));
        }

        public IActionResult ShowDetails (int productId) {
            return View (productsDAO.GetProductById(productId));
        }

        public IActionResult Edit(int productId)
        {
            productId2 = productId;
            return View("ShowEditForm",productsDAO.GetProductById(productId));
        }

        public IActionResult ProcessEdit (Product product) {
            product.Id = productId2;
            productsDAO.Update(product);
            return View ("Index", productsDAO.GetAllProducts());
        }

        public IActionResult Delete (int productId)
        {
            productsDAO.Delete(productsDAO.GetProductById(productId));
            return View ("Index", productsDAO.GetAllProducts());
        }

        public IActionResult SearchForm ()
        {
            return View ();
        }

        public IActionResult CreateProduct () {
            return View ("ShowCreateForm");
        }

        public IActionResult ProcessCreate (Product product) {
            productsDAO.Insert(product);
            return View("Index", productsDAO.GetAllProducts());
        }

        public IActionResult ListSortedProducts (Product product) {
            return View("Index", productsDAO.GetAllProductsSorted());
        }
    }
}
