using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ITeamService
    {
        Task<bool> CanJoinTeamAsync(string gameExternalId, string teamExternalId);

        Task<TeamGameResponse> GetTeamGameAsync(string gameExternalId, string teamExternalId);
    }
}