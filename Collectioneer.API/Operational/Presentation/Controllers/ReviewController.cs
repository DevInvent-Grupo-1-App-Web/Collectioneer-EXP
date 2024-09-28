using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
	[ApiController]
	public class ReviewController(
		ILogger<ReviewController> logger,
		IReviewService reviewService,
		IContentModerationService contentModerationService,
		ICommentService commentService
		) : ControllerBase
	{
		private readonly ILogger<ReviewController> _logger = logger;
		private readonly IReviewService _reviewService = reviewService;
		private readonly IContentModerationService contentModerationService = contentModerationService;
		private readonly ICommentService _commentService = commentService;

		[Authorize]
		[HttpPost("collectible/new-review")]
		public async Task<ActionResult<ReviewDTO>> CreateReview([FromBody] ReviewCreateCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Content}"))
				{
					throw new ExposableException("Contenido inapropiado detectado.", 400);
				}
				var review = await _reviewService.CreateReview(request);
				return ReviewDTO.FromReview(review);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating review.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("collectible/{id}/reviews")]
		public async Task<ActionResult<ICollection<ReviewDTO>>> GetCollectibleReviews([FromRoute]int id)
		{
			try
			{
				var reviews = await _reviewService.GetCollectibleReviews(new CollectibleReviewsQuery(id));
				return reviews.Select(ReviewDTO.FromReview).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting reviews.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("user/{id}/reviews")]
		public async Task<ActionResult<ICollection<ReviewDTO>>> GetUserReviews([FromRoute]int id)
		{
			try
			{
				var reviews = await _reviewService.GetUserReviews(new UserReviewsQuery(id));
				return reviews.Select(ReviewDTO.FromReview).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting reviews.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("review/{id}/new-comment")]
		public async Task<ActionResult<ReviewDTO>> CreateComment([FromRoute]int id, [FromBody] CommentRegisterCommand command)
		{
			try
			{
				command.ReviewId = id;
				await _commentService.PostComment(command);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating comment.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("review/{id}/comments")]
		public async Task<ActionResult<ICollection<CommentDTO>>> GetComments([FromRoute]int id)
		{
			try
			{
				var comments = await _commentService.GetCommentsForReview(id);
				return Ok(comments);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting comments.");
				return StatusCode(500, ex.Message);
			}
		}

	}
}