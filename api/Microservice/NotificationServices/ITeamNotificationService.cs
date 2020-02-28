namespace Microservice.NotificationServices
{
    public interface ITeamNotificationService
    {
        void SendItemFoundNotification(string gameExternalId, string teamExternalId, string collectableItemExternalId);

        void SendSightedNotification(string gameExternalId, string teamExternalId, string guardExternalId);
    }
}