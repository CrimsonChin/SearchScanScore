using System.Threading.Tasks;
using CodeHunt.Domain.Models;

namespace CodeHunt.Domain.Services
{
    public interface IGameService
    {
        Game Get(string gameExternalId);
        Task StartGame(string gameExternalId);
        Task StopGame(string gameExternalId);
        Task Reset(string gameExternalId);
    }
}