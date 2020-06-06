using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ITeamService
    {
        Task<bool> CanJoinTeamAsync(string gameCode, string teamCode);

        Task<TeamResponse> GetTeamAsync(string gameCode, string teamCode);
    }
}