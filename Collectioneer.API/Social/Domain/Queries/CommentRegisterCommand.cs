namespace Collectioneer.API.Social.Domain.Queries
{
	public record CommentRegisterCommand(
		int AuthorId,
		string Content
	)
	{
		public int AuthorId { get; init; } = AuthorId;
		public int? PostId { get; set; } = null;
		public int? CommentId { get; set; } = null;
		public int? CollectibleId { get; set; } = null;
		public int? ReviewId { get; set; } = null;
		public string Content { get; init; } = Content;
	}
}