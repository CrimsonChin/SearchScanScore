using System;

namespace Data.Sql.Entities
{
    public class CollectedItem
    {
        public int CollectedItemId { get; set; }

        public DateTime CollectedAt { get; set; }

        public Team Team { get; set; }
        public CollectableItem CollectableItem { get; set; }
    }
}
