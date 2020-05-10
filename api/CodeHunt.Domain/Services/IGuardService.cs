using System.Threading.Tasks;

namespace CodeHunt.Domain.Services
{
    public interface IGuardService
    {
        Task AddSighting(string gameExternalId, string guardExternalId, string teamExternalId);
    }
}
