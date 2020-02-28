using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.Hubs
{
    public class AdminHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("AdminMessage", message);
        }
    }
}