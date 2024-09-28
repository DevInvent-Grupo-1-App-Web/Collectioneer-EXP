using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
	public class ReviewRepository(AppDbContext context) : BaseRepository<Review>(context), IReviewRepository
	{
		public async Task<ICollection<Review>> GetCollectibleReviews(int collectibleId)
		{
			return await _context.Reviews.Where(r => r.CollectibleId == collectibleId).ToListAsync();
		}

		public async Task<ICollection<Review>> GetUserReviews(int userId)
		{
			return await _context.Reviews.Where(r => r.ReviewerId == userId).ToListAsync();
		}
	}
}