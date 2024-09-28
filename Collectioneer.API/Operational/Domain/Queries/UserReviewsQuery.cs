namespace Collectioneer.API.Operational.Domain.Queries
{
	public record UserReviewsQuery(int UserId) {
		public int UserId { get; init; } = UserId;
	}
}