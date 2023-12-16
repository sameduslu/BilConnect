using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        public string SenderUserId { get; set; }
        public int ChatId { get; set; }

    }
}
