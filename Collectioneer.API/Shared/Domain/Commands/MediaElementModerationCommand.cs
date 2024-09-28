namespace Collectioneer.API.Shared.Domain.Commands
{
	public record MediaElementModerationCommand
	(
		int UploaderId,
		string MediaName,
		string MediaModeratorKey
	) {
		public int UploaderId { get; init; } = UploaderId;
		public string MediaName { get; init; } = MediaName;
		public string MediaModeratorKey { get; init; } = MediaModeratorKey;
	}
}