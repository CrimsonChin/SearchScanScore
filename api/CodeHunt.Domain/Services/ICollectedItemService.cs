using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public interface ICollectedItemService
    {
        Task AddCollectedItemAsync(string gameCode, string teamCode, string itemCode);

        Task<IEnumerable<CollectedItemResponse>> GetCollectedItemsAsync(string gameCode, string teamCode);
    }
}