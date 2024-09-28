using Collectioneer.API.Operational.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Application.External
{
    public class BidDTO(Bid bid)
	{
		public int Id { get; init; } = bid.Id;
		public int AuctionId { get; init; } = bid.AuctionId;
		public float Amount { get; init; } = bid.Amount;
		public DateTime CreatedAt { get; init; } = bid.CreatedAt;
	}
}