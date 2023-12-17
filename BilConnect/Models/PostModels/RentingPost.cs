namespace BilConnect.Models.PostModels
{
    public class RentingPost : Post
    {
        public double Price { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
