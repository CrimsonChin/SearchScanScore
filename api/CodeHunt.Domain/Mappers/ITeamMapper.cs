using System.Collections.Generic;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface ITeamMapper
    {
        IEnumerable<TeamResponse> Map(IEnumerable<Team> entities);
    }
}