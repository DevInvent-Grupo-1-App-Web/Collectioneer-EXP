using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Models.Aggregates
{
	public class Post(
		int communityId,
		string title,
		string content,
		int authorId
		) : ICommentable, IReactable, ITimestamped
	{
		public int Id { get; set; }
		public int CommunityId { get; set; } = communityId;
		public string Title { get; set; } = title;
		public string Content { get; set; } = content;
		public int AuthorId { get; set; } = authorId;
		public bool IsHidden { get; set; } = false;
		public bool IsArchived { get; set; } = false;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public Community? Community { get; set; }
		public User? Author { get; set; }
		public ICollection<PostTag> PostTags { get; set; } = [];
		public ICollection<MediaElement> MediaElements { get; set; } = [];
		public ICollection<Comment> Comments { get; set; } = [];
		public ICollection<Reaction> Reactions { get; set; } = [];
	}
}