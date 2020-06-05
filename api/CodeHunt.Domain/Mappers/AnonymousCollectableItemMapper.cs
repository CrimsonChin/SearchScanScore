using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public class AnonymousCollectableItemMapper : IAnonymousCollectableItemMapper
    {
        public IEnumerable<AnonymousCollectableItemResponse> Map(IEnumerable<Entities.CollectableItem> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<AnonymousCollectableItemResponse>();
            }

            return entities.Select(Map);
        }

        private static AnonymousCollectableItemResponse Map(Entities.CollectableItem entity)
        {
            return new AnonymousCollectableItemResponse
            {
                Name = entity.Name
            };
        }
    }
}
