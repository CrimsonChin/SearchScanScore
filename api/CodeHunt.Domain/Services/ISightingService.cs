using System.Threading.Tasks;

namespace CodeHunt.Domain.Services
{
    public interface ISightingService
    {
        Task AddSightingAsync(string gameExternalId, string guardExternalId, string teamExternalId);
    }
}
