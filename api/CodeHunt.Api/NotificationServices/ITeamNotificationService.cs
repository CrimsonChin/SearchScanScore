using System.Threading.Tasks;

namespace CodeHunt.Api.NotificationServices
{
    public interface ITeamNotificationService
    {
        Task SendItemFoundNotification(string gameExternalId, string teamExternalId, string collectableItemExternalId);

        Task SendSightedNotification(string gameExternalId, string teamExternalId, string guardExternalId);
    }
}