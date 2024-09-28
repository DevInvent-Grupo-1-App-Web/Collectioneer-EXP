using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
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
	public class CollectibleController(
		ICollectibleService collectibleService,
		IContentModerationService contentModerationService,
		ILogger<CollectibleController> logger, 
		ICommentService commentService
		) : ControllerBase
	{
		private readonly ICollectibleService _collectibleService = collectibleService;
		private readonly ICommentService _commentService = commentService;
		private readonly IContentModerationService contentModerationService = contentModerationService;
		private readonly ILogger<CollectibleController> _logger = logger;

		[HttpGet("collectibles")]
		public async Task<ActionResult<ICollection<CollectibleDTO>>> GetCollectibles([FromQuery] CollectibleBulkRetrieveQuery request)
		{
			try
			{
				var response = await _collectibleService.GetCollectibles(request);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting collectibles.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("collectibles/{id}")]
		public async Task<ActionResult<CollectibleDTO>> GetCollectible([FromRoute] int id)
		{
			try
			{
				var response = await _collectibleService.GetCollectible(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting collectible.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("collectibles")]
		public async Task<ActionResult<CollectibleDTO>> CreateCollectible([FromBody] CollectibleRegisterCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Name} {request.Description}"))
				{
					throw new ExposableException("Contenido inapropiado detectado.", 400);
				}

				var collectible = await _collectibleService.RegisterCollectible(request);
				return CreatedAtAction(nameof(GetCollectible), new { id = collectible.Id }, collectible);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating collectible.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("search/collectibles")]
		public async Task<ActionResult<ICollection<CollectibleDTO>>> SearchCollectibles([FromQuery] CollectibleSearchQuery query)
		{
			try
			{
				var response = await _collectibleService.SearchCollectibles(query);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error searching collectibles.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("collectible/{id}/comments")]
		public async Task<ActionResult<ICollection<CommentDTO>>> GetCommentsForCollectible([FromRoute] int id)
		{
			try
			{
				var response = await _commentService.GetCommentsForCollectible(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting comments for collectible.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("collectible/{id}/comments")]
		public async Task<ActionResult<CommentDTO>> CreateCommentForCollectible([FromRoute] int id, [FromBody] CommentRegisterCommand request)
		{
			try
			{
				request.CollectibleId = id;
				await _commentService.PostComment(request);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating comment for collectible.");
				return StatusCode(500, ex.Message);
			}
		}
	}
}