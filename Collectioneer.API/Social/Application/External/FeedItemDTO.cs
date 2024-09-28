using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Application.External
{
	public record FeedItemDTO(
		int Id,
		ICollection<string> MultimediaURI,
		string Title,
		string Content,
		DateTime CreatedAt,
		string Username,
		int UserId,
		int CommunityId,
		string CommunityName,
		string ItemType
	)
	{
		public int Id { get; init; } = Id;
		public ICollection<string> MultimediaURI { get; init; } = MultimediaURI;
		public string Title { get; init; } = Title;
		public string Content { get; init; } = Content;
		public DateTime CreatedAt { get; init; } = CreatedAt;
		public string Username { get; init; } = Username;
		public int UserId { get; init; } = UserId;
		public int CommunityId { get; init; } = CommunityId;
		public string CommunityName { get; init; } = CommunityName;
		public string ItemType { get; init; } = ItemType;
	}
}