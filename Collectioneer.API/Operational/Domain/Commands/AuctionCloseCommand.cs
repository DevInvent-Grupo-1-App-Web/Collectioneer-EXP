namespace Collectioneer.API;

public record AuctionCloseCommand
(
	int AuctionId
)
{
	public int AuctionId { get; init; } = AuctionId;
}
