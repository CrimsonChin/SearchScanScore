using System.Collections.Generic;
using SearchScanScore.Services.Models;

namespace SearchScanScore.Services.Interfaces
{
    public interface ITeamService
    {
        void CollectItem(string gameExternalId, string teamExternalId, string itemExternalId);
        
        IList<CollectedItem> GetCollectedItems(string gameExternalId, string teamExternalId);

        IEnumerable<Sighting> GetSightings(string gameExternalId, string teamExternalId);
    }
}