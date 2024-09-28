namespace Collectioneer.API.Operational.Domain.Commands
{
	public record CollectibleRegisterCommand
		(
			string Name,
			int CommunityId,
			string Description,
			int UserId,
			float? Value
		)
	{
		public int CommunityId { get; init; } = CommunityId;
		public string Name { get; init; } = Name;
		public string Description { get; init; } = Description;
		public int UserId { get; init; } = UserId;
		public float? Value { get; init; } = Value;
	}
}
