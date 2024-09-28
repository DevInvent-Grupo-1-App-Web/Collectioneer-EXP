namespace Collectioneer.API.Operational.Domain.Commands
{
    public record AuctionValidationCommand
		(
			int AuctionId,
            bool AsBidder,
            bool AsAuctioneer
		)
    {
        public int AuctionId { get; init; } = AuctionId;
        public bool AsBidder { get; init; } = AsBidder;
        public bool AsAuctioneer { get; init; } = AsAuctioneer;
    }
}
