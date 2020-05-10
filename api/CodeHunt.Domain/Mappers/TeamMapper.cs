using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamMapper : ITeamMapper
    {
        public IEnumerable<TeamResponse> Map(IEnumerable<Team> entities)
        {
            return entities.Select(Map);
        }

        private static TeamResponse Map(Team entity)
        {
            return new TeamResponse
            {
                ExternalId = entity.ExternalId,
                Name = entity.Name
            };
        }
    }
}
