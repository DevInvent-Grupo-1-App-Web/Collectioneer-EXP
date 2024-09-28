using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class Filter(
		int communityId,
		string name,
		int filterTypeId
		)
	{
		public int Id { get; set; }
		public int CommunityId { get; set; } = communityId;
		public string Name { get; set; } = name;
		public int FilterTypeId { get; set; } = filterTypeId;

		// Navigation properties
		public Community? Community { get; set; }
		public FilterType? FilterType { get; set; }
	}
}