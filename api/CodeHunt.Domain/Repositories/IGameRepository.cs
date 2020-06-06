using System.Threading.Tasks;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface IGameRepository : IRepository
    {
        Task<Game> GetAsync(string gameCode);
        
        Game Update(Game game);

        void Reset(string gameCode);
    }
}