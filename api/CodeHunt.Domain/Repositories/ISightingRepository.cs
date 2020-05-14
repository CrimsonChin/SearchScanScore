using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface ISightingRepository : IRepository
    {
        Sighting Add(Sighting sighting);

        Task<IEnumerable<Sighting>> GetAsync(string gameExternalId, string teamExternalId);
    }
}
