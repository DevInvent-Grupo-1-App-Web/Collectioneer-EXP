using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Domain.Repositories
{
    public interface ICollectibleRepository : IBaseRepository<Collectible>
    {
		public Task DeleteUserCollectibles(int userId);
        public Task<ICollection<Collectible>> GetCollectibles(int communityId, int maxAmount=-1, int offset=0);
		public Task<ICollection<Collectible>> Search(string searchTerm, int communityId = 0);
    }
}
