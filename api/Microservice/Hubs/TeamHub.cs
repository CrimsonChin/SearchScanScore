using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.Hubs
{
    public class TeamHub : Hub
    {
        public async Task Join(string teamExternalId)
        {
            // TODO validation
            await Groups.AddToGroupAsync(Context.ConnectionId, teamExternalId);
        }
    }
}