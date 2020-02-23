using System.Collections.Generic;
using SearchScanScore.Services.Models;

namespace SearchScanScore.Services.Interfaces
{
    public interface ITeamService
    {
        void CollectItem(string gameExternalId, string teamExternalId, string itemExternalId);
        int GetTeamScore(string gameExternalId, string teamExternalId);
        IList<CollectedItem> GetCollectedItems(string gameExternalId, string teamExternalId);
    }
}