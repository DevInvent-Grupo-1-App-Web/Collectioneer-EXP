namespace Collectioneer.API.Operational.Domain.Commands
{
	public record class AuctionCreationCommand
	(
		int CommunityId,
		int AuctioneerId,
		int CollectibleId,
		float StartingPrice,
		DateTime Deadline
	)
	{
		public int CommunityId { get; init; } = CommunityId;
		public int AuctioneerId { get; init; } = AuctioneerId;
		public int CollectibleId { get; init; } = CollectibleId;
		public float StartingPrice { get; init; } = StartingPrice;
		public DateTime Deadline { get; init; } = Deadline;
	}
}
