using System.Collections.Generic;

namespace CodeHunt.Domain.Models
{
    public class TeamStats
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Sighting> Sightings { get; set; }

        public int TotalItemsCollected { get; set; }

        public IEnumerable<CollectedItem> ItemsCollected { get; set; }

        public IEnumerable<CollectableItem> RemainingItems { get; set; }
    }
}
