using System.Collections.Generic;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface ISightingRepository : IRepository
    {
        Sighting Add(Sighting sighting);

        IEnumerable<Sighting> Get(string gameExternalId, string teamExternalId);
    }
}
