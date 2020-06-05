using System.Collections.Generic;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface ITeamSightingMapper
    {
        IEnumerable<SightingResponse> Map(IEnumerable<Entities.Sighting> entities);
    }
}