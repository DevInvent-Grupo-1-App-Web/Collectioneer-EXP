using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Domain.Repositories {

	public interface IReviewRepository : IBaseRepository<Review> {
		public Task<ICollection<Review>> GetCollectibleReviews(int collectibleId);
		public Task<ICollection<Review>> GetUserReviews(int userId);
	}
}