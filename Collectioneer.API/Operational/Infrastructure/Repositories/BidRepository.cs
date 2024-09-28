using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
    public class BidRepository(AppDbContext context) : BaseRepository<Bid>(context), IBidRepository
    {
		public async Task<IEnumerable<Bid>> GetBidsByAuctionId(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId).ToListAsync();
        }
    }
}
