using Collectioneer.API.Operational.Domain.Models.Aggregates;

namespace Collectioneer.API.Operational.Application.External
{
    public record AuctionDTO
    {
        public int Id { get; init; }
        public int CommunityId { get; init; }
        public int AuctioneerId { get; init; }
        public int CollectibleId { get; init; }
        public float StartingPrice { get; init; }
        public DateTime Deadline { get; init; }
        public bool IsOpen { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public AuctionDTO(Auction auction)
        {
            Id = auction.Id;
            CommunityId = auction.CommunityId;
            AuctioneerId = auction.AuctioneerId;
            CollectibleId = auction.CollectibleId;
            StartingPrice = auction.CurrentPrice();
            Deadline = auction.Deadline;
            IsOpen = auction.IsOpen;
            CreatedAt = auction.CreatedAt;
            UpdatedAt = auction.UpdatedAt;
        }
    }
}
