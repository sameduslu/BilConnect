using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.AccessControl;

namespace BilConnect.Models.PostModels
{
    public class EventTicketPost : Post
    {
        public string EventTime { get; set; }

        public string EventPlace { get; set; }

        public double Price { get; set; }

    }
}
