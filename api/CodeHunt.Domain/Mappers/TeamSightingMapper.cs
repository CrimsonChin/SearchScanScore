using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamSightingMapper : ITeamSightingMapper
    {
        public IEnumerable<TeamSightingResponse> Map(IEnumerable<Sighting> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<TeamSightingResponse>();
            }

            return entities.Select(Map);
        }

        private static TeamSightingResponse Map(Sighting entity)
        {
            return new TeamSightingResponse
            {
                SightedAt = entity.SightedAt,
                SightedBy = entity.Guard.Name
            };
        }
    }
}
