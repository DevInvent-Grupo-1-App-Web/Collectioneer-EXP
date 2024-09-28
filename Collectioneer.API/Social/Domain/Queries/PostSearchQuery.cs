namespace Collectioneer.API.Social.Domain.Queries
{
    public record PostSearchQuery (
        string SearchTerm,
        int CommunityId)
    {
        public string SearchTerm { get; init; } = SearchTerm;
        public int CommunityId { get; init;  } = CommunityId;
    }
}
