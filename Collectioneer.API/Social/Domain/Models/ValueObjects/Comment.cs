using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Comment : IReactable, ITimestamped, ICommentable
	{
		public int Id { get; set; }
		public int AuthorId { get; set; }
		public int? CollectibleId { get; set; }
		public int? PostId { get; set; }
		public int? ParentCommentId { get; set; }
		public int? ReviewId { get; set; }
		public string Content { get; set; } = string.Empty;	
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsHidden { get; set; } = false;

		// Navigation properties
		public Collectible? Collectible { get; set; }
		public Post? Post { get; set; }
		public Comment? ParentComment { get; set; }
		public User? Author { get; set; }
		public Review? Review { get; set; }
		public ICollection<Comment> Comments { get; set; } = [];
		public ICollection<Reaction> Reactions { get; set; } = [];

		public Comment()
		{
		}

		public Comment(
			int parentElementId,
			Type parentElementType,
			int authorId,
			string content
		)
		{
			if (parentElementType == typeof(Collectible))
			{
				CollectibleId = parentElementId;
			}
			else if (parentElementType == typeof(Post))
			{
				PostId = parentElementId;
			}
			else if (parentElementType == typeof(Comment))
			{
				ParentCommentId = parentElementId;
			}
			else if (parentElementType == typeof(Review))
			{
				ReviewId = parentElementId;
			}

			AuthorId = authorId;
			Content = content;
		}
	}
}