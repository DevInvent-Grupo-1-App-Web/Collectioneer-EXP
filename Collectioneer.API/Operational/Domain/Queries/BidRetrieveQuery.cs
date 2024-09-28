namespace Collectioneer.API.Operational.Domain.Queries
{
    public record BidRetrieveQuery
	(
		int AuctionId
	)
    {
        public int AuctionId { get; set; } = AuctionId;
    }
}
