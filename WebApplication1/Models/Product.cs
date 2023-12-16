using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Product
    {
        public Product() {
            Id = -1;
        }
        public int Id { get; set; }

        [DisplayName("Name Of The Product")]
        public string Name { get; set; }
        [DisplayName("Description Of The Product")]
        public string Description { get; set; }
        [DisplayName("Price Of The Product")]
        [DataType (DataType.Currency)]
        public decimal Price { get; set; }
    }
}
