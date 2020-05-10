using System.Threading.Tasks;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface ITeamRepository : IRepository
    {
        Task<Team> Get(int teamId);
    }
}