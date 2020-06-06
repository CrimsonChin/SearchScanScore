using System;

namespace CodeHunt.Domain.Responses
{
    public class CollectedItemResponse
    {
        public Guid ExternalId { get; set; }

        public string CollectableItemName { get; set; }

        public Guid CollectableItemExternalId { get; set; }

        public DateTime CollectedAt { get; set; }
    }
}
