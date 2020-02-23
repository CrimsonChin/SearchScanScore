using SearchScanScore.Services.Models;

namespace SearchScanScore.Services.Interfaces
{
    public interface IGameService
    {
        Game Get(string gameExternalId);
        void StartGame(string gameExternalId);
        void StopGame(string gameExternalId);
        void ResetCollectedItems(string gameExternalId);
    }
}