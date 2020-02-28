using System.Threading.Tasks;
using Microservice.NotificationServices;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.Hubs
{
    public class TeamHub : Hub
    {
        public async Task Join(string gameExternalId, string teamExternalId)
        {
            // TODO validation
            var key = TeamKey.Generate(gameExternalId, teamExternalId);
            await Groups.AddToGroupAsync(Context.ConnectionId, key);
        }
    }
}