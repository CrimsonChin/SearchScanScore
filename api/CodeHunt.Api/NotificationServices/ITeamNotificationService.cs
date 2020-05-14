using System.Threading.Tasks;

namespace CodeHunt.Api.NotificationServices
{
    public interface ITeamNotificationService
    {
        Task SendItemFoundNotificationAsync(string gameExternalId, string teamExternalId, string collectableItemExternalId);

        Task SendSightedNotificationAsync(string gameExternalId, string teamExternalId, string guardExternalId);
    }
}