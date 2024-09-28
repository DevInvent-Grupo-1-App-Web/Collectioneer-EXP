namespace Collectioneer.API.Operational.Domain.Queries
{
    public record AuctionGetByCollectibleIdQuery(
        int CollectibleId    
    )
    {
        public int CollectibleId { get; init; } = CollectibleId;
    }
}
