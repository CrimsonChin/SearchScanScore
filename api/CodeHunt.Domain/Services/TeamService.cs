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

        public async Task<bool> CanJoinTeamAsync(string gameCode, string teamCode)
        {
            var game = await _gameRepository.GetAsync(gameCode);

            var team = game?.Teams.FirstOrDefault(x => x.Code == teamCode);
            return team != null;
        }

        public async Task<TeamResponse> GetTeamAsync(string gameCode, string teamCode)
        {
            var game = await _gameRepository.GetAsync(gameCode);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No game found with code: {gameCode}");
            }

            if (game.IsActive == false)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No active game found with code: {gameCode}");
            }

            var team = game.Teams.SingleOrDefault(x => x.Code == teamCode);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound,
                    $"No team found with code: {teamCode}");
            }

            team = await _teamRepository.Get(team.TeamId);
            var response = _teamMapper.Map(team);

            return response;
        }
    }
}
