using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    internal class GuardMapper : IGuardMapper
    {
        public IEnumerable<GuardResponse> Map(IEnumerable<Guard> entities)
        {
            return entities.Select(Map);
        }

        private static GuardResponse Map(Guard entity)
        {
            return new GuardResponse
            {
                ExternalId = entity.ExternalId,
                Name = entity.Name
            };
        }
    }
}
