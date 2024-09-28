using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Reaction : ITimestamped
	{
		public int Id { get; set; }
		public int? PostId { get; set; }
		public int? CommentId { get; set; }
		public int? CollectibleId { get; set; }
		public int UserId { get; set; }
		public int ReactionTypeId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? User { get; set; }
		public Post? Post { get; set; }
		public Comment? Comment { get; set; }
		public Collectible? Collectible { get; set; }

		public Reaction() {}

        public Reaction(
			int reactableId,
			Type reactableType,
			int userId,
			int reactionTypeId
		)
		{
			if (reactableType == typeof(Post))
			{
				PostId = reactableId;
			}
			else if (reactableType == typeof(Comment))
			{
				CommentId = reactableId;
			}
			else if (reactableType == typeof(Collectible))
			{
				CollectibleId = reactableId;
			}
			
			UserId = userId;
			ReactionTypeId = reactionTypeId;
		}
	}
}