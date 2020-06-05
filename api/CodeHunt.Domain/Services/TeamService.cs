using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CodeHunt.Domain.Exceptions;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMapper _teamMapper;

        public TeamService(
            IGameRepository gameRepository, 
            ITeamRepository teamRepository, 
            ITeamMapper teamMapper)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _teamMapper = teamMapper;
        }

        public async Task<bool> CanJoinTeamAsync(string gameExternalId, string teamExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);

            var team = game?.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            return team != null;
        }

        public async Task<TeamResponse> GetTeamAsync(string gameExternalId, string teamExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No active game found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No team found with external Id: {teamExternalId}");
            }

            team = await _teamRepository.Get(team.TeamId);
            var response = _teamMapper.Map(team);

            return response;
        }
    }
}
