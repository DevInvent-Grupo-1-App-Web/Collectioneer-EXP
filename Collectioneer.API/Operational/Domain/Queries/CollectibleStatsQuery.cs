namespace Collectioneer.API.Operational.Domain.Queries {
	public record CollectibleStatsQuery(int CollectibleId) {
		public int CollectibleId { get; init; } = CollectibleId;
	}
}