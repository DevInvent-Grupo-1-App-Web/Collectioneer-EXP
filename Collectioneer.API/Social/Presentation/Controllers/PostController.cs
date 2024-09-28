using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Social.Presentation.Controllers
{
	[ApiController]
	public class PostController(
		IPostService postService,
		IContentModerationService contentModerationService, 
		ICommentService commentService,
		ILogger<PostController> logger
		) : ControllerBase
	{
		[Authorize]

		[HttpPost("new-post")]
		public async Task<ActionResult<PostDTO>> AddPost([FromBody] AddPostCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Title} {request.Content}"))
				{
					throw new ExposableException("Contenido inapropiado detectado.", 400);
				}

				var newPost = await postService.AddPost(request);
				return Ok(newPost);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error creating post.");
				return StatusCode(500, ex.Message);
			}

		}

		[HttpGet("posts/{postId}")]
		public async Task<ActionResult<PostDTO>> GetPost([FromRoute] int postId)
		{
            try
			{
                var post = await postService.GetPost(postId) ?? throw new Exception("Post not found.");
                return Ok(post);
            }
            catch (Exception ex)
			{
                logger.LogError(ex, "Error getting post.");
                return StatusCode(500, ex.Message);
            }
        }

		[HttpGet("search/posts")]
		public async Task<ActionResult<ICollection<PostDTO>>> SearchPosts([FromQuery] PostSearchQuery query)
		{
            try
			{
                var posts = await postService.Search(query);
                return Ok(posts);
            }
            catch (Exception ex)
			{
                logger.LogError(ex, "Error searching posts.");
                return StatusCode(500, ex.Message);
            }
		}

		[HttpGet("post/{id}/comments")]
		public async Task<ActionResult<ICollection<CommentDTO>>> GetCommentsForPost([FromRoute] int id)
		{
			try
			{
				var comments = await commentService.GetCommentsForPost(id);
				return Ok(comments);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting comments for post.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("post/{id}/comment")]
		public async Task<ActionResult<CommentDTO>> CreateCommentForPost([FromRoute] int id, [FromBody] CommentRegisterCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Content}"))
				{
					throw new ExposableException("Contenido inapropiado detectado.", 400);
				}

				request.PostId = id;
				await commentService.PostComment(request);
				return Ok();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error creating comment for post.");
				return StatusCode(500, ex.Message);
			}
		}
	}

}