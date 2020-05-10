using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamCollectedItemMapper : ITeamCollectedItemMapper
    {
        public IEnumerable<TeamCollectedItemResponse> Map(IEnumerable<Entities.CollectedItem> entities)
        {
            return entities.Select(Map);
        }

        private static TeamCollectedItemResponse Map(Entities.CollectedItem entity)
        {
            return new TeamCollectedItemResponse
            {
                CollectableItemName = entity.CollectableItem.Name,
                CollectedAt = entity.CollectedAt
            };
        }
    }
}
