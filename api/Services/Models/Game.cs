using System.Collections.Generic;
using System.Linq;

namespace SearchScanScore.Services.Models
{
    public class Game
    {
        public Game()
        {
            Teams = Enumerable.Empty<Team>();
            Guards = Enumerable.Empty<Guard>();
            CollectableItems = Enumerable.Empty<CollectableItem>();
        }

        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        public IEnumerable<CollectableItem> CollectableItems { get; set; }

        public IEnumerable<Guard> Guards { get; set; }

        public bool IsActive { get; set; }
    }
}
