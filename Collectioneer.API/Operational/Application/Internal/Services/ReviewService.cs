using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Operational.Application.Internal.Services
{
	public class ReviewService(
		IReviewRepository reviewRepository,
		IUnitOfWork unitOfWork
	) : IReviewService
	{
		private readonly IReviewRepository _reviewRepository = reviewRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<Review> CreateReview(ReviewCreateCommand command)
		{
			var review = new Review(
				command.ReviewerId,
				command.CollectibleId,
				command.Content,
				command.Rating
			);
			await _reviewRepository.Add(review);
			await _unitOfWork.CompleteAsync();
			return review;
		}

		public async Task<ICollection<Review>> GetCollectibleReviews(CollectibleReviewsQuery query)
		{
			return await _reviewRepository.GetCollectibleReviews(query.CollectibleId);
		}

		public async Task<(float, int)> GetCollectibleStats(CollectibleStatsQuery query)
		{
			var reviews = await _reviewRepository.GetCollectibleReviews(query.CollectibleId);

			if (reviews.Count == 0)
			{
				return (0, 0);
			}

			var ratingSum = 0;
			foreach (var review in reviews)
			{
				ratingSum += review.Rating;
			}

			var averageRating = (float)ratingSum / reviews.Count;
			return (averageRating, reviews.Count);
		}

		public Task<ICollection<Review>> GetUserReviews(UserReviewsQuery query)
		{
			return _reviewRepository.GetUserReviews(query.UserId);
		}
	}
}