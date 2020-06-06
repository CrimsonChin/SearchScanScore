using System;

namespace CodeHunt.Domain.Entities
{
    public class CollectedItem
    {
        public int CollectedItemId { get; set; }

        public Guid ExternalId { get; set; }

        public DateTime CollectedAt { get; set; }

        public Team Team { get; set; }
        public CollectableItem CollectableItem { get; set; }
    }
}
