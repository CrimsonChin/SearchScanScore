using System.Threading.Tasks;
using CodeHunt.Api.NotificationServices;
using Microsoft.AspNetCore.SignalR;

namespace CodeHunt.Api.Hubs
{
    public class TeamHub : Hub
    {
        public async Task Join(string gameExternalId, string teamExternalId)
        {
            // Add them to a "Game" group.  So the admin can message all teams
            await Groups.AddToGroupAsync(Context.ConnectionId, gameExternalId);

            // Add them to a "Team" group so messages can be aimed at a specific team.
            var key = NotificationKeyFactory.GetTeamKey(gameExternalId, teamExternalId);
            await Groups.AddToGroupAsync(Context.ConnectionId, key);
        }

        // TODO authorize this so only admins can use it
        public async Task MessageAllTeamsInGame(string gameExternalId, string message)
        {
            await Clients.Group(gameExternalId).SendAsync("AdminMessage", message);
        }
    }
}