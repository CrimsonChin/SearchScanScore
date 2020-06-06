using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ISightingService
    {
        Task AddSightingAsync(string gameCode, string guardCode, string teamCode);

        Task<IEnumerable<SightingResponse>> GetSightingsAsync(string gameCode, string teamCode);
    }
}
