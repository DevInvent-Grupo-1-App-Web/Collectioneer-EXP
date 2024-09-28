using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Exceptions;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Collectible : ITimestamped, IReactable
	{
		public int Id { get; set; }
		// Entity properties
		public int CommunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public int OwnerId { get; set; }
		public float? Value { get; set; }
		public string Description { get; set; } = string.Empty;
		public int? AuctionId { get; set; }
		public int? SaleId { get; set; }
		public int? ExchangeId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Owner { get; set; }
		public Community? Community { get; set; }
		public Auction? Auction { get; set; }
		public Sale? Sale { get; set; }
		public Exchange? Exchange { get; set; }
		public ICollection<Review> Reviews { get; set; } = [];
		public ICollection<MediaElement> MediaElements { get; set; } = [];
		public ICollection<Comment> Comments { get; set; } = [];
		public ICollection<Reaction> Reactions { get; set; } = [];

		public Collectible(
			int communityId,
			string name,
			string description,
			int ownerId,
			float? value
		)
		{
			CommunityId = communityId;
			SetName(name);
			Description = description;
			SetOwnerId(ownerId);
			SetValue(value);
		}

		public bool IsLinkedToProcess()
		{
			// Verify if in auction
			return AuctionId != null;

			// TODO: Verify if in other processes when implemented
		}

		private void SetName(string name)
		{
			if (name.Length < 3 || name.Length > 100)
			{
				throw new CollectibleModelException("Name must be between 1 and 100 characters");
			}
			Name = name;
		}

		private void SetOwnerId(int ownerId)
		{
			OwnerId = ownerId;
		}

		private void SetValue(float? value)
		{
			Value = value ?? null;
		}
	}
}
