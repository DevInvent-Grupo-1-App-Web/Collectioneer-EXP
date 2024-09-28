namespace Collectioneer.API.Shared.Domain.Commands
{
	public record MediaElementUploadCommand(
		string MediaName,
		string Content,
		string ContentType,
		int AttachedToId,
		string AttachedToType
	)
	{
		public string MediaName { get; set; } = MediaName;
		public string Content { get; set; } = Content;
		public string ContentType { get; set; } = ContentType;
		public int AttachedToId { get; set; } = AttachedToId;
		public string AttachedToType { get; set; } = AttachedToType;
	}
}