namespace Collectioneer.API.Operational.Domain.Queries
{
    public record AuctionBulkRetrieveQuery(
        int CommunityId,
        int MaxAmount = -1,
        int Offset = 0
    )
    {
        public int CommunityId { get; init; } = CommunityId;
        public int MaxAmount { get; init; } = MaxAmount;
        public int Offset { get; init; } = Offset;
    }
}
