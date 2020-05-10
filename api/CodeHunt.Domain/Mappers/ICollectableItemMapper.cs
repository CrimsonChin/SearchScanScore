using System.Collections.Generic;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    internal interface ICollectableItemMapper
    {
        IEnumerable<CollectableItemResponse> Map(IEnumerable<CollectableItem> entities);
    }
}