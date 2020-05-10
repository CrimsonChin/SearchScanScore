using System.Collections.Generic;

namespace CodeHunt.Domain.Responses
{
    public class TeamGameResponse
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeamSightingResponse> Sightings { get; set; }

        public IEnumerable<TeamCollectedItemResponse> ItemsCollected { get; set; }

        public IEnumerable<TeamCollectableItemResponse> RemainingItems { get; set; }
    }
}
