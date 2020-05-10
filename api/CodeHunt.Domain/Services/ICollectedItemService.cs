using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ICollectedItemService
    {
        Task AddCollectedItem(string gameExternalId, string teamExternalId, string itemExternalId);
        IEnumerable<TeamCollectedItemResponse> GetCollectedItems(string gameExternalId, string teamExternalId);
    }
}