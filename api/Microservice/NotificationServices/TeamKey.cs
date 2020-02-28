namespace Microservice.NotificationServices
{
    public static class TeamKey
    {
        public static string Generate(string gameExternalId, string teamExternalId)
        {
            return $"{gameExternalId}-{teamExternalId}";
        }
    }
}
