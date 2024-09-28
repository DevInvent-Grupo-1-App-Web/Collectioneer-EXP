namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleAuctionIdRegisterCommand
		(
			int CollectibleId,
			int AuctionId
		)
    {
        public int CollectibleId { get; init; } = CollectibleId;
        public int AuctionId { get; init; } = AuctionId;
    }
}
