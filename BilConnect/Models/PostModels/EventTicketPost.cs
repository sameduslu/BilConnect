using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.AccessControl;

namespace BilConnect.Models.PostModels
{
    public class EventTicketPost : Post
    {
        DateTime EventTime { get; set; }

        string EventPlace { get; set; }

        double Price { get; set; }

    }
}
