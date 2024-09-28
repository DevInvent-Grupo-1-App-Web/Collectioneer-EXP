using System.Collections;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services
{
	public class CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IMediaElementRepository mediaElementRepository, IUnitOfWork unitOfWork) : ICommentService
	{
		public async Task<ICollection<CommentDTO>> GetCommentsForCollectible(int collectibleId)
		{
			var comments = await commentRepository.GetCommentsForCollectible(collectibleId);
			var commentDTOs = comments.Select(c => MapCommentToDTO(c).Result).ToList();
			return commentDTOs;
		}

		public async Task<ICollection<CommentDTO>> GetCommentsForComment(int commentId)
		{
			var comments = await commentRepository.GetCommentsForComment(commentId);
			var commentDTOs = comments.Select(c => MapCommentToDTO(c).Result).ToList();
			return commentDTOs;
		}

		public async Task<ICollection<CommentDTO>> GetCommentsForPost(int postId)
		{
			var comments = await commentRepository.GetCommentsForPost(postId);
			var commentDTOs = comments.Select(c => MapCommentToDTO(c).Result).ToList();
			return commentDTOs;
		}

		public async Task<ICollection<CommentDTO>> GetCommentsForReview(int reviewId)
		{
			var comments = await commentRepository.GetCommentsForReview(reviewId);
			var commentDTOs = comments.Select(c => MapCommentToDTO(c).Result).ToList();
			return commentDTOs;
		}

		public async Task<ICollection<CommentDTO>> GetCommentsForUser(int userId)
		{
			var comments = await commentRepository.GetCommentsForUser(userId);
			var commentDTOs = comments.Select(c => MapCommentToDTO(c).Result).ToList();
			return commentDTOs;
		}

		public async Task PostComment(CommentRegisterCommand command)
		{
			(Type, int) commentParent = command switch
			{
				{ CollectibleId: not null } => (typeof(Collectible), command.CollectibleId.Value),
				{ CommentId: not null } => (typeof(Comment), command.CommentId.Value),
				{ PostId: not null } => (typeof(Post), command.PostId.Value),
				{ ReviewId: not null } => (typeof(Review), command.ReviewId.Value),
				_ => throw new ArgumentException("Invalid comment parent type.")
			};

			var comment = new Comment(
				commentParent.Item2,
				commentParent.Item1,
				command.AuthorId,
				command.Content
			);
			
			await commentRepository.Add(comment);
			await unitOfWork.CompleteAsync();
		}

		private async Task<CommentDTO> MapCommentToDTO(Comment comment)
		{
			var author = await userRepository.GetById(comment.AuthorId);
			var mediaElement = mediaElementRepository.GetAll().Result.Where(m => m.UploaderId == comment.AuthorId && m.ProfileId is not null).FirstOrDefault();

			return new CommentDTO(
				comment.Id,
				comment.AuthorId,
				author is null ? "Anonymous" : author.Username,
				comment.Content,
				mediaElement is null ? "" : mediaElement.MediaURL
			);
		}
	}
}