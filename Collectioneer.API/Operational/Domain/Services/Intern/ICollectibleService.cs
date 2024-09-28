using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;

namespace Collectioneer.API.Operational.Domain.Services.Intern
{
    public interface ICollectibleService
    {
        public Task<CollectibleDTO> RegisterCollectible(CollectibleRegisterCommand command);
        public Task RegisterAuctionIdInCollectible(CollectibleAuctionIdRegisterCommand command);
        public Task<CollectibleDTO> GetCollectible(int id);
        public Task<ICollection<CollectibleDTO>> GetCollectibles(CollectibleBulkRetrieveQuery command);
		public Task<ICollection<CollectibleDTO>> SearchCollectibles(CollectibleSearchQuery query);
    }
}
