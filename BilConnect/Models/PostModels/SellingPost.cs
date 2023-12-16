using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models.PostModels
{
    public class SellingPost : Post
    {
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
