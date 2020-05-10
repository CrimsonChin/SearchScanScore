using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ITeamService
    {
        bool CanJoinTeam(string gameExternalId, string teamExternalId);

        Task<TeamGameResponse> GetTeamGame(string gameExternalId, string teamExternalId);
    }
}