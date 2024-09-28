namespace Collectioneer.API.Social.Domain.Commands
{
	public record AddPostCommand(
		string Title,
		string Content,
		int CommunityId,
		int AuthorId
	)
	{
		public string Title { get; init; } = Title;
		public string Content { get; init; } = Content;
		public int CommunityId { get; init; } = CommunityId;
		public int AuthorId { get; init; } = AuthorId;
	}
}