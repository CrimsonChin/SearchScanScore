using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class CollectedItemMapper : ICollectedItemMapper
    {
        public IEnumerable<CollectedItemResponse> Map(IEnumerable<Entities.CollectedItem> entities)
        {
            return entities?.Select(Map);
        }

        private static CollectedItemResponse Map(Entities.CollectedItem entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CollectedItemResponse
            {
                CollectableItemName = entity.CollectableItem.Name,
                CollectedAt = entity.CollectedAt
            };
        }
    }
}
