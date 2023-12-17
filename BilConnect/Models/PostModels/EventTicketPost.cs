using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.AccessControl;

namespace BilConnect.Models.PostModels
{
    public class EventTicketPost : Post
    {
        public DateTime? EventTime { get; set; }

        public string EventPlace { get; set; }

        public double Price { get; set; }

    }
}
