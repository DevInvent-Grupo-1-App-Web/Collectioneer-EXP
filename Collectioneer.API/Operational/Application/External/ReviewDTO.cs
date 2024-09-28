using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Application.External
{
	public record ReviewDTO(int Id, int UserId, int CollectibleId, string Content, int Rating, DateTime CreatedAt, DateTime UpdatedAt)
	{
		public int Id { get; init; } = Id;
		public int UserId { get; init; } = UserId;
		public int CollectibleId { get; init; } = CollectibleId;
		public string Content { get; init; } = Content;
		public int Rating { get; init; } = Rating;
		public DateTime CreatedAt { get; init; } = CreatedAt;
		public DateTime UpdatedAt { get; init; } = UpdatedAt;

		public static ReviewDTO FromReview(Review review)
		{
			return new ReviewDTO(
				review.Id,
				review.ReviewerId,
				review.CollectibleId,
				review.Content,
				review.Rating,
				review.CreatedAt,
				review.UpdatedAt
			);
		}
	}


}