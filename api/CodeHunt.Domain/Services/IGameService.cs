using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface IGameService
    {
        Task<GameResponse> GetAsync(string gameCode);
        Task StartGameAsync(string gameCode);
        Task StopGameAsync(string gameCode);
        Task ResetAsync(string gameCode);
    }
}