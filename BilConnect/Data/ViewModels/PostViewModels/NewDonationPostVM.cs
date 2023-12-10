namespace BilConnect.Data.ViewModels.PostViewModels
{
    public class NewDonationPostVM : NewPostVM
    {
        DateTime EventTime { get; set; }

        string EventPlace { get; set; }

        double Price { get; set; }
    }
}
