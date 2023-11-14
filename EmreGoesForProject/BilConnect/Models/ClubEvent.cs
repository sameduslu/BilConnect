using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ClubEvent
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public DateTime PostDate { get; set; }

    }
}
