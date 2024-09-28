using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Tag(
		int communityId,
		string name,
		string value
		)
	{
		public int Id { get; set; }
		public int CommunityId { get; set; } = communityId;
		public string Name { get; set; } = name;
		public string Value { get; set; } = value;

		// Navigation properties
		public Community? Community { get; set; }
		public ICollection<PostTag> PostTags { get; set; } = [];
	}
}