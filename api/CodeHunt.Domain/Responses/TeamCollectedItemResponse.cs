using System;

namespace CodeHunt.Domain.Responses
{
    public class TeamCollectedItemResponse
    {
        public string CollectableItemName { get; set; }
        public DateTime CollectedAt { get; set; }
    }
}
