using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;

namespace Collectioneer.API.Operational.Domain.Services.Intern
{
	public interface IReviewService
	{
		public Task<Review> CreateReview(ReviewCreateCommand command);
		public Task<ICollection<Review>> GetCollectibleReviews(CollectibleReviewsQuery query);
		public Task<ICollection<Review>> GetUserReviews(UserReviewsQuery query);
		public Task<(float, int)> GetCollectibleStats(CollectibleStatsQuery query);
	}
}