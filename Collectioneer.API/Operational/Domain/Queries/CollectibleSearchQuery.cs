namespace Collectioneer.API.Operational.Domain.Queries
{
	public record CollectibleSearchQuery(
		string SearchTerm,
		int CommunityId = 0
	)
	{
		public string SearchTerm { get; init; } = SearchTerm;
		public int CommunityId { get; init; } = CommunityId;
	}
}