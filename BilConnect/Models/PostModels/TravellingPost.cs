namespace BilConnect.Models.PostModels
{
    public class TravellingPost : Post
    {
        public  string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime? TravelTime { get; set; }

        public double Price { get; set; }

        public int Quota { get; set; }

    }
}
