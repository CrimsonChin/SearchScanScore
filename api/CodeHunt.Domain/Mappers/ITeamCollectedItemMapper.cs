using System.Collections.Generic;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface ITeamCollectedItemMapper
    {
        IEnumerable<TeamCollectedItemResponse> Map(IEnumerable<Entities.CollectedItem> entities);
    }
}