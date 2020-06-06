using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    internal class GameMapper : IGameMapper
    {
        private readonly ITeamMapper _teamMapper;
        private readonly IGuardMapper _guardMapper;
        private readonly ICollectableItemMapper _collectableItemMapper;

        public GameMapper(ITeamMapper teamMapper, IGuardMapper guardMapper, ICollectableItemMapper collectableItemMapper)
        {
            _teamMapper = teamMapper;
            _guardMapper = guardMapper;
            _collectableItemMapper = collectableItemMapper;
        }

        public GameResponse Map(Game game)
        {
            if (game == null)
            {
                return null;
            }

            return new GameResponse
            {
                ExternalId = game.ExternalId,
                Code = game.Code,
                Name = game.Name,
                IsActive = game.IsActive,
                Teams = _teamMapper.Map(game.Teams),
                Guards = _guardMapper.Map(game.Guards),
                CollectableItems = _collectableItemMapper.Map(game.CollectableItems)
            };
        }
    }
}
