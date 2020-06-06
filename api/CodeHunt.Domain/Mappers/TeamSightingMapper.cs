using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamSightingMapper : ITeamSightingMapper
    {
        public IEnumerable<SightingResponse> Map(IEnumerable<Sighting> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<SightingResponse>();
            }

            return entities.Select(Map);
        }

        private static SightingResponse Map(Sighting entity)
        {
            return new SightingResponse
            {
                ExternalId = entity.ExternalId,
                SightedAt = entity.SightedAt,
                SightedBy = entity.Guard.Name
            };
        }
    }
}
