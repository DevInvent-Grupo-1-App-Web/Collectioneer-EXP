namespace Collectioneer.API.Social.Domain.Queries
{
	public record CommunitySearchContentQuery(
		string SearchTerm,
		int CommunityId
	) {
		public string SearchTerm { get; init; } = SearchTerm;
		public int CommunityId { get; init; } = CommunityId;
	}
}