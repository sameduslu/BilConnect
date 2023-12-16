namespace WebApplication1.Models
{
    public class ClubEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place {  get; set; }
        public ClubAccount organizingClub { get; set; }
        public string Poster { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int GEPoints {  get; set; }

    }
}
