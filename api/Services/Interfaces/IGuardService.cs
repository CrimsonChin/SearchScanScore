namespace SearchScanScore.Services.Interfaces
{
    public interface IGuardService
    {
        void RecordSighting(string gameExternalId, string guardExternalI, string teamExternalId);
    }
}
