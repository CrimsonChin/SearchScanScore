using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface IGameService
    {
        GameResponse Get(string gameExternalId);
        Task StartGame(string gameExternalId);
        Task StopGame(string gameExternalId);
        Task Reset(string gameExternalId);
    }
}