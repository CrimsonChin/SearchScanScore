using System.Threading.Tasks;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface IGameRepository : IRepository
    {
        Task<Game> GetAsync(string gameExternalId);
        
        Game Update(Game game);

        void Reset(string gameExternalId);
    }
}