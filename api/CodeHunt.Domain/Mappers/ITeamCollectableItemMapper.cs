using System.Collections.Generic;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface ITeamCollectableItemMapper
    {
        IEnumerable<TeamCollectableItemResponse> Map(IEnumerable<Entities.CollectableItem> entities);
    }
}