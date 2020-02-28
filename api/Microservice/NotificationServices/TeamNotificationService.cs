using Microservice.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.NotificationServices
{
    public class TeamNotificationService : NotificationService<TeamHub>, ITeamNotificationService
    {
        public TeamNotificationService(IHubContext<TeamHub> teamHubContext)
            : base(teamHubContext)
        {
        }

        void ITeamNotificationService.SendItemFoundNotification(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            var key = TeamKey.Generate(gameExternalId, teamExternalId);
            HubContext.Clients.Group(key).SendAsync("ItemFound", collectableItemExternalId);
        }

        void ITeamNotificationService.SendSightedNotification(string gameExternalId, string teamExternalId, string guardExternalId)
        {
            var key = TeamKey.Generate(gameExternalId, teamExternalId);
            HubContext.Clients.Group(key).SendAsync("Sighted", guardExternalId);
        }
    }
}
