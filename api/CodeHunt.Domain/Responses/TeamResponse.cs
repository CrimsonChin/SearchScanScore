using System.Collections.Generic;

namespace CodeHunt.Domain.Responses
{
    public class TeamResponse
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public IEnumerable<SightingResponse> Sightings { get; set; }

        public IEnumerable<CollectedItemResponse> CollectedItems { get; set; }
    }
}
