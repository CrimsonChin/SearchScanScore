using System;

namespace SearchScanScore.Services.Models
{
    public class CollectedItem
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string CollectableItemId { get; set; }
        public string CollectableItemName { get; set; }
        public DateTime CollectedAt { get; set; }
    }
}
