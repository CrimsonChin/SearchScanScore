using System.Threading.Tasks;

namespace CodeHunt.Domain.Services
{
    public interface ISightingService
    {
        Task AddSighting(string gameExternalId, string guardExternalId, string teamExternalId);
    }
}
