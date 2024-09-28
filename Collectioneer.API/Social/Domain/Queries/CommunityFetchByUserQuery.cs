namespace Collectioneer.API.Social.Domain.Queries
{
	public class CommunityFetchByUserQuery(int userId)
	{
		public int UserId { get; set; } = userId;
	}
}