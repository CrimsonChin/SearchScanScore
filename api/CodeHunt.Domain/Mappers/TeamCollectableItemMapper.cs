using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class TeamCollectableItemMapper : ITeamCollectableItemMapper
    {
        public IEnumerable<TeamCollectableItemResponse> Map(IEnumerable<Entities.CollectableItem> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<TeamCollectableItemResponse>();
            }

            return entities.Select(Map);
        }

        private static TeamCollectableItemResponse Map(Entities.CollectableItem entity)
        {
            return new TeamCollectableItemResponse
            {
                Name = entity.Name
            };
        }
    }
}
