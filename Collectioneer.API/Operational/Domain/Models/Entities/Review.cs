using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Operational.Domain.Models.Entities
{
	public class Review(
		int reviewerId,
		int collectibleId,
		string content,
		int rating
		) : ITimestamped
	{
		public int Id { get; set; }
		public int ReviewerId { get; set; } = reviewerId;
		public int CollectibleId { get; set; } = collectibleId;
		public string Content { get; set; } = content;
		public int Rating { get; set; } = rating;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Reviewer { get; set; }
		public Collectible? ReviewedCollectible { get; set; }
		public ICollection<Comment>? Comments { get; set; }
	}

}

