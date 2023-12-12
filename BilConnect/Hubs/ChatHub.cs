using Microsoft.AspNetCore.SignalR;

namespace BilConnect.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMEssage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
