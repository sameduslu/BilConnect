using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels
{
    public class ChatVM
    {
        public int Id { get; set; }
        public int RelatedPostId { get; set; }
        public string ReceiverId { get; set; }
        public string UserId { get; set; }

    }
}
