using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
	public class CommentRepository(AppDbContext context) : BaseRepository<Comment>(context), ICommentRepository
	{
		public async Task<ICollection<Comment>> GetCommentsForCollectible(int collectibleId)
		{
			return await _context.Comments
				.Where(c => c.CollectibleId == collectibleId)
				.ToListAsync();
		}

		public async Task<ICollection<Comment>> GetCommentsForComment(int commentId)
		{
			return await _context.Comments
				.Where(c => c.ParentCommentId == commentId)
				.ToListAsync();
		}

		public async Task<ICollection<Comment>> GetCommentsForPost(int postId)
		{
			return await _context.Comments
				.Where(c => c.PostId == postId)
				.ToListAsync();
		}

		public async Task<ICollection<Comment>> GetCommentsForReview(int reviewId)
		{
			return await _context.Comments
				.Where(c => c.ReviewId == reviewId)
				.ToListAsync();
		}

		public async Task<ICollection<Comment>> GetCommentsForUser(int userId)
		{
			return await _context.Comments
				.Where(c => c.AuthorId == userId)
				.ToListAsync();
		}
	}
}