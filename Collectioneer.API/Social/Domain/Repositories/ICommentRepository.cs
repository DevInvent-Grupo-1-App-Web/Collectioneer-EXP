using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Repositories
{
	public interface ICommentRepository : IBaseRepository<Comment>
	{
		public Task<ICollection<Comment>> GetCommentsForComment(int commentId);
		public Task<ICollection<Comment>> GetCommentsForCollectible(int collectibleId);
		public Task<ICollection<Comment>> GetCommentsForPost(int postId);
		public Task<ICollection<Comment>> GetCommentsForUser(int userId);
		public Task<ICollection<Comment>> GetCommentsForReview(int reviewId);
	}
}