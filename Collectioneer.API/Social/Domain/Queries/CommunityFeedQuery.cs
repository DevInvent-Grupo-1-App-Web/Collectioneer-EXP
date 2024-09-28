namespace Collectioneer.API.Social.Domain.Queries
{
	public class CommunityFeedQuery(int communityId)
	{
		public int CommunityId { get; set; } = communityId;
	}
}