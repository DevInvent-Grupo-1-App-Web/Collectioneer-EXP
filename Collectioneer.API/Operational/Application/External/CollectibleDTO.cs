using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Application.External
{
    public record CollectibleDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int OwnerId { get; init; }
        public float? Value { get; init; }
        public string Description { get; init; }
        public bool IsLinkedToProcess { get; init; }
        public int? AuctionId { get; init; }
        public int? SaleId { get; init; }
        public int? ExchangeId { get; init; }
		public float Rating { get; set; }
		public int ReviewCount { get; set; }

        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public CollectibleDTO(Collectible collectible)
        {
            Id = collectible.Id;
            Name = collectible.Name;
            OwnerId = collectible.OwnerId;
            Value = collectible.Value;
            Description = collectible.Description;
            IsLinkedToProcess = collectible.IsLinkedToProcess();
            AuctionId = collectible.AuctionId;
            SaleId = collectible.SaleId;
            ExchangeId = collectible.ExchangeId;
            CreatedAt = collectible.CreatedAt;
            UpdatedAt = collectible.UpdatedAt;
        }
    }
}
