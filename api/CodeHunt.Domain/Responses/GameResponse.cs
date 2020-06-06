using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeHunt.Domain.Responses
{
    public class GameResponse
    {
        public GameResponse()
        {
            Teams = Enumerable.Empty<TeamResponse>();
            Guards = Enumerable.Empty<GuardResponse>();
            CollectableItems = Enumerable.Empty<CollectableItemResponse>();
        }

        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeamResponse> Teams { get; set; }

        public IEnumerable<CollectableItemResponse> CollectableItems { get; set; }

        public IEnumerable<GuardResponse> Guards { get; set; }

        public bool IsActive { get; set; }
    }
}
