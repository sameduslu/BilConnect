namespace BilConnect.Models.PostModels
{
    public class BorrowingPost : Post
    {
        double Price { get; set; }
        DateTime ReturnDate { get; set; }
    }
}
