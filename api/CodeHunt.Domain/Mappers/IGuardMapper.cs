using System.Collections.Generic;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    internal interface IGuardMapper
    {
        IEnumerable<GuardResponse> Map(IEnumerable<Guard> entities);
    }
}