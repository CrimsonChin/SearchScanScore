using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    internal class CollectableItemMapper : ICollectableItemMapper
    {
        public IEnumerable<CollectableItemResponse> Map(IEnumerable<CollectableItem> entities)
        {
            return entities.Select(Map);
        }

        private static CollectableItemResponse Map(CollectableItem entity)
        {
            return new CollectableItemResponse
            {
                ExternalId = entity.ExternalId,
                Name = entity.Name
            };
        }
    }
}
