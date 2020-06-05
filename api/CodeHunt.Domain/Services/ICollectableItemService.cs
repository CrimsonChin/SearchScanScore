using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ICollectableItemService
    {
        Task<IEnumerable<AnonymousCollectableItemResponse>> GetAnonymousAsync(string gameExternalId);

        Task<IEnumerable<AnonymousCollectableItemResponse>> GetRemainingAsync(string gameExternalId, string teamExternalId);
    }
}