namespace Collectioneer.API.Operational.Domain.Commands
{
	public record ReviewCreateCommand(
		int ReviewerId,
		int CollectibleId,
		string Content,
		int Rating
	) {
		public int ReviewerId { get; init; } = ReviewerId;
		public int CollectibleId { get; init; } = CollectibleId;
		public string Content { get; init; } = Content;
		public int Rating { get; init; } = Rating;
	}
}