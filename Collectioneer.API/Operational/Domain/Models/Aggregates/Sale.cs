using Collectioneer.API.Operational.Domain.Interfaces;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Domain.Models.Aggregates;

public class Sale(
	int vendorId,
	int collectibleId,
	float price
	) : ITimestamped, ITransaction
{
	public int Id { get; set; }
	public int CommunityId { get; set; }
	public int VendorId { get; set; } = vendorId;
	public int CollectibleId { get; set; } = collectibleId;
	public int? BuyerId { get; set; }
	public float Price { get; set; } = price;
	public bool IsOpen { get; set; } = true;
	public bool VendorHasCollected { get; set; } = false;
	public bool BuyerHasCollected { get; set; } = false;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	// Navigation properties
	public User? Vendor { get; set; }
	public User? Buyer { get; set; }
	public Community? Community { get; set; }
	public Collectible? Collectible { get; set; }
}
