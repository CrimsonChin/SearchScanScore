using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;
using CodeHunt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly CodeHuntContext _context;

        public TeamRepository(CodeHuntContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task<Team> Get(int teamId)
        {
            var team = _context.Teams
                .Where(x => x.TeamId == teamId)
                .Include(x => x.Sightings)
                .ThenInclude(y => y.Guard)
                .Include(x => x.CollectedItems)
                .ThenInclude(y => y.CollectableItem)
                .FirstOrDefaultAsync();

            return team;
        }
    }
}
