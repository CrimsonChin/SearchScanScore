using System.Collections.Generic;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface ICollectedItemMapper
    {
        IEnumerable<CollectedItemResponse> Map(IEnumerable<Entities.CollectedItem> entities);
    }
}