using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface IGameService
    {
        Task<GameResponse> GetAsync(string gameExternalId);
        Task StartGameAsync(string gameExternalId);
        Task StopGameAsync(string gameExternalId);
        Task ResetAsync(string gameExternalId);
    }
}