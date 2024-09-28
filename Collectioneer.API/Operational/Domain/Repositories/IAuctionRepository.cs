using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Collectioneer.API.Operational.Domain.Repositories
{
    public interface IAuctionRepository : IBaseRepository<Auction>
    {
		public new Task<Auction?> GetById(int id);
        public Task<Auction> GetByCollectibleId(int collectibleId);
        public Task<IEnumerable<Auction>> GetAuctions(int CommunityId, int MaxAmount, int Offset);
        public Task<Auction> GetAuctionByCollectibleId(int CollectibleId);
    }
}
