using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface IGameRepository : IRepository
    {
        Game Get(string gameExternalId);
        
        Game Update(Game game);

        void Reset(string gameExternalId);
    }
}