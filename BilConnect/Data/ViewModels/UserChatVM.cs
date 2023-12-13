using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels
{
    public class UserChatVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatId { get; set; }

    }
}
