using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.ValueObjects
{
	public class Bid(
		int auctionId,
		int bidderId,
		float amount
		) : ITimestamped
	{
		public int Id { get; set; }
		public int AuctionId { get; set; } = auctionId;
		public int BidderId { get; set; } = bidderId;
		public float Amount { get; set; } = amount;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public Auction? Auction { get; set; }
		public User? Bidder { get; set; }
	}
}