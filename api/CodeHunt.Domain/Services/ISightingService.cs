using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ISightingService
    {
        Task AddSightingAsync(string gameExternalId, string guardExternalId, string teamExternalId);

        Task<IEnumerable<SightingResponse>> GetSightingsAsync(string gameExternalId, string teamExternalId);
    }
}
