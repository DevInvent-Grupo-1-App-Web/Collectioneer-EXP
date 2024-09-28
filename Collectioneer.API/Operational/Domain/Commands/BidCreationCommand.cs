namespace Collectioneer.API.Operational.Domain.Commands
{
	public record class BidCreationCommand
	(
		int AuctionId,
		int BidderId,
		float Amount
	)
	{
		public int AuctionId { get; init; } = AuctionId;
		public int BidderId { get; init; } = BidderId;
		public float Amount { get; init; } = Amount;
	}

}
