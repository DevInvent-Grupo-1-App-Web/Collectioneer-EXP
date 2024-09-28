using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Exceptions;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
    public class AuctionRepository(AppDbContext context, ICollectibleRepository collectibleRepository) : BaseRepository<Auction>(context), IAuctionRepository
    {
		public async Task<Auction> GetAuctionByCollectibleId(int CollectibleId)
        {
            try
            {
                var collectible = await collectibleRepository.GetById(CollectibleId);
                var auction = await _context.Auctions
                    .AsNoTracking()
					.Include(a => a.Bids)
                    .FirstOrDefaultAsync(a => a.CollectibleId == CollectibleId);

                return auction ?? throw new EntityNotFoundException("Auction not found");
            }
            catch (Exception)
            {
                throw new EntityNotFoundException("Auction not found");
            }
        }

        public async Task<IEnumerable<Auction>> GetAuctions(int CommunityId, int MaxAmount, int Offset)
        {
            var query = _context.Auctions.Include(a => a.Bids).Where(a => a.CommunityId == CommunityId);

            if (Offset > 0)
            {
                query = query.Skip(Offset);
            }

            if (MaxAmount != -1)
            {
                query = query.Take(MaxAmount);
            }

            return await query.ToListAsync();
        }

        public async Task<Auction> GetByCollectibleId(int collectibleId)
        {
            var collectible = await _context.Collectibles
			.FirstOrDefaultAsync(c => c.Id == collectibleId);

            if (collectible == null)
            {
                throw new EntityNotFoundException("Collectible not found");
            }

            if (collectible.AuctionId == null)
            {
                throw new EntityNotFoundException("Collectible is not being auctioneed.");
            }

            var auction = await _context.Auctions
                            .AsNoTracking()
							.Include(a => a.Bids)
                            .FirstOrDefaultAsync(a => a.Id == collectible.AuctionId);

            if (auction == null)
            {
                throw new EntityNotFoundException("Auction not found");
            }

            return auction;



        }

        public new async Task<Auction?> GetById(int id)
        {

            return await _context.Auctions
                .AsNoTracking()
				.Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id) ?? throw new EntityNotFoundException("Auction not found");
        }

    }
}
