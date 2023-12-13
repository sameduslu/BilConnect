namespace BilConnect.Data.ViewModels.PostViewModels
{
    public class NewTravellingPostVM : NewPostVM
    {
        string Origin { get; set; }

        string Destination { get; set; }

        DateTime TravelTime { get; set; }

        double Price { get; set; }

        int Quota { get; set; }
    }
}
