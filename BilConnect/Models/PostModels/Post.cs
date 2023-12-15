using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models.PostModels
{
    public class Post : IEntityBase, IComparable<Post>
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Post Description")]
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public DateTime PostDate { get; set; }

        public PostStatus PostStatus { get; set; }

        //Relationshpis
        public string UserId { get; set; }
        [ForeignKey("UserId")]


        // Navigation properties
        public virtual ApplicationUser? User { get; set; }
        public List<Chat>? Chats { get; set; }

        // Compares posts based on the time.
        public int CompareTo(Post other)
        {
            if (other == null)
            {
                return 1; // If the other object is null, this instance is greater.
            }
            return other.PostDate.CompareTo(PostDate);
        }

    }
}
