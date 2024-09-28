using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Queries;

namespace Collectioneer.API.Social.Domain.Services
{
	public interface ICommentService
	{
		Task<ICollection<CommentDTO>> GetCommentsForCollectible(int collectibleId);
		Task<ICollection<CommentDTO>> GetCommentsForComment(int commentId);
		Task<ICollection<CommentDTO>> GetCommentsForPost(int postId);
		Task<ICollection<CommentDTO>> GetCommentsForUser(int userId);
		Task<ICollection<CommentDTO>> GetCommentsForReview(int reviewId);
		Task PostComment(CommentRegisterCommand command);
	}
}