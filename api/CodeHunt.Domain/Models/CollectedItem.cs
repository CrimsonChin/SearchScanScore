using System;

namespace CodeHunt.Domain.Models
{
    public class CollectedItem
    {
        public string CollectableItemExternalId { get; set; }
        public string CollectableItemName { get; set; }
        public DateTime CollectedAt { get; set; }
    }
}
