using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Mappers
{
    public interface IGameMapper
    {
        GameResponse Map(Game game);
    }
}