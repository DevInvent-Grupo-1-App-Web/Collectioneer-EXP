using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Application.External
{
	public class PostDTO(Post post)
	{
		public int Id { get; set; } = post.Id;
		public string Title { get; set; } = post.Title;
		public string Content { get; set; } = post.Content;
		public DateTime CreatedAt { get; set; } = post.CreatedAt;
		public DateTime UpdatedAt { get; set; } = post.UpdatedAt;
		public int AuthorId { get; set; } = post.AuthorId;
	}
}