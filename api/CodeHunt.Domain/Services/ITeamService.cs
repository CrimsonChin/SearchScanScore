using System.Threading.Tasks;
using CodeHunt.Domain.Models;

namespace CodeHunt.Domain.Services
{
    public interface ITeamService
    {
        bool CanJoinTeam(string gameExternalId, string teamExternalId);

        Task AddCollectedItem(string gameExternalId, string teamExternalId, string itemExternalId);
        
        Task<TeamStats> GetTeamStats(string gameExternalId, string teamExternalId);
    }
}