using System.Collections.Generic;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface IAnonymousCollectableItemMapper
    {
        IEnumerable<AnonymousCollectableItemResponse> Map(IEnumerable<Entities.CollectableItem> entities);
    }
}