using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamMapper : ITeamMapper
    {
        private readonly ITeamSightingMapper _teamSightingMapper;
        private readonly ICollectedItemMapper _collectedItemMapper;

        public TeamMapper(ITeamSightingMapper teamSightingMapper, ICollectedItemMapper collectedItemMapper)
        {
            _teamSightingMapper = teamSightingMapper;
            _collectedItemMapper = collectedItemMapper;
        }

        public IEnumerable<TeamResponse> Map(IEnumerable<Team> entities)
        {
            return entities.Select(Map);
        }

        public TeamResponse Map(Team entity)
        {
            return new TeamResponse
            {
                ExternalId = entity.ExternalId,
                Code = entity.Code,
                Name = entity.Name,
                Sightings = _teamSightingMapper.Map(entity.Sightings),
                CollectedItems = _collectedItemMapper.Map(entity.CollectedItems),
            };
        }
    }
}
