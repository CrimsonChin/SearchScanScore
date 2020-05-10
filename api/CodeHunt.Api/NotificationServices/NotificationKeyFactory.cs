namespace CodeHunt.Api.NotificationServices
{
    public static class NotificationKeyFactory
    {
        public static string GetTeamKey(string gameExternalId, string teamExternalId) => $"{gameExternalId}-{teamExternalId}";
    }
}
