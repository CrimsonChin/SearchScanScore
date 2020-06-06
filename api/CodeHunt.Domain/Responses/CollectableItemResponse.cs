using System;

namespace CodeHunt.Domain.Responses
{
    public class CollectableItemResponse
    {
        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
