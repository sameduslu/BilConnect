namespace BilConnect.Models.PostModels
{
    public class TravellingPost : Post
    {
        string Origin { get; set; }

        string Destination { get; set; }

        DateTime TravelTime { get; set; }

        double Price { get; set; }

        int Quota { get; set; }

    }
}
