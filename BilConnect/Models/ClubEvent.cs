using BilConnect.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class ClubEvent : IEntityBase, IComparable<ClubEvent>
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Activity Name")]
        public string Name { get; set; }
        [DisplayName ("Starting Time")]
        [DataType(DataType.DateTime)]
        public DateTime? startTime { get; set; }
        [DisplayName ("Ending Time")]
        [DataType(DataType.DateTime)]
        public DateTime? endTime { get; set; }
        [DisplayName("Quota")]
        public int quota { get; set; }
        [ForeignKey("ownerClubId")]
        public string ownerClubId { get; set; }
        [DisplayName ("Organizer Club")]
        public virtual ApplicationUser? ownerClub { get; set; }
        public string Description { get; set; }
        [DisplayName("GE250/251 Points")]
        public int? GE250_251Points { get; set; }
        [DisplayName("Will GE250/251 Points Will Be Given")]
        public bool GE250_251Status { get; set; }
        public string Place {  get; set; }
        [DisplayName("Image URL")]
        public string ImageURL { get; set; }
        public DateTime CreationTime { get; set; }

        public int CompareTo (ClubEvent clubEvent)
        {
            if(clubEvent == null) return 1;
            return clubEvent.CreationTime.CompareTo(CreationTime);
        }
        
    }
}
