using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ICollectedItemService
    {
        Task AddCollectedItemAsync(string gameExternalId, string teamExternalId, string itemExternalId);

        Task<IEnumerable<TeamCollectedItemResponse>> GetCollectedItemsAsync(string gameExternalId, string teamExternalId);
    }
}