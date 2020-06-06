using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ICollectableItemService
    {
        Task<IEnumerable<AnonymousCollectableItemResponse>> GetAnonymousAsync(string gameCode);

        Task<IEnumerable<AnonymousCollectableItemResponse>> GetRemainingAsync(string gameCode, string teamCode);
    }
}