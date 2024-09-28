using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Interfaces
{
	public interface ICommentable
	{
		public ICollection<Comment> Comments { get; set; }
	}
}