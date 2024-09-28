namespace Collectioneer.API.Social.Application.External
{
	public record CommentDTO
	(
		int Id,
		int UserId,
		string Username,
		string Content,
		string ProfileURI
	) {
		public int Id { get; init; } = Id;
		public int UserId { get; init; } = UserId;
		public string Username { get; init; } = Username;
		public string Content { get; init; } = Content;
		public string ProfileURI { get; init; } = ProfileURI;
	}
}