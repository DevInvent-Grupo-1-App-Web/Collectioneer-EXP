using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Domain.Repositories
{
    public interface IBidRepository : IBaseRepository<Bid>
    {
        public Task<IEnumerable<Bid>> GetBidsByAuctionId(int auctionId);
    }
}
