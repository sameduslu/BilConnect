using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BilConnect.Models.PostModels
{
    public class Post : IEntityBase
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Post Description")]
        public string Description { get; set; }

        public string ImageURL { get; set; }

        // This property is used to store the JSON data in the database
        public string AdditionalImagesJson { get; set; }

        // This property is not mapped to the database and is used in your application
        [NotMapped]
        public List<string> AdditionalImages
        {
            get => string.IsNullOrEmpty(AdditionalImagesJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(AdditionalImagesJson);
            set => AdditionalImagesJson = JsonSerializer.Serialize(value);
        }

        public DateTime PostDate { get; set; }

        public PostStatus PostStatus { get; set; }

        //Relationshpis
        public string UserId { get; set; }
        [ForeignKey("UserId")]


        // Navigation properties
        public virtual ApplicationUser? User { get; set; }
        public List<Chat>? Chats { get; set; }

    }
}
