using System.Threading.Tasks;
using CodeHunt.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CodeHunt.Api.NotificationServices
{
    public class TeamNotificationService : NotificationService<TeamHub>, ITeamNotificationService
    {
        public TeamNotificationService(IHubContext<TeamHub> teamHubContext)
            : base(teamHubContext)
        {
        }

        async Task ITeamNotificationService.SendItemFoundNotification(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            var key = NotificationKeyFactory.GetTeamKey(gameExternalId, teamExternalId);
            await HubContext.Clients.Group(key).SendAsync("ItemFound", collectableItemExternalId);
        }

        async Task ITeamNotificationService.SendSightedNotification(string gameExternalId, string teamExternalId, string guardExternalId)
        {
            var key = NotificationKeyFactory.GetTeamKey(gameExternalId, teamExternalId);
            await HubContext.Clients.Group(key).SendAsync("Sighted", guardExternalId);
        }
    }
}
