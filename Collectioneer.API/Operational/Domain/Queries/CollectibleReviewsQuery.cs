namespace Collectioneer.API.Operational.Domain.Queries
{
	public record CollectibleReviewsQuery(int CollectibleId) {
		public int CollectibleId { get; init; } = CollectibleId;
	}
}