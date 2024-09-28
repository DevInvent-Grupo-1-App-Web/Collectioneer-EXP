using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class PostTag(
		int postId,
		int tagId
		)
	{
		public int Id { get; set; }
		public int PostId { get; set; } = postId;
		public int TagId { get; set; } = tagId;

		// Navigation properties
		public Post? Post { get; set; }
		public Tag? Tag { get; set; }
	}
}