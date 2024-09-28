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
	public class CommunityController(ICommunityService communityService,
	IContentModerationService contentModerationService,
	ILogger<CommunityController> logger) : ControllerBase
	{
		[Authorize]
		[HttpPost("new-community")]
		public async Task<ActionResult<CommunityDTO>> CreateCommunity([FromBody] CommunityCreateCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Name} {request.Description}"))
				{
					throw new ExposableException("Contenido inapropiado detectado.", 400);
				}

				var newCommunity = await communityService.CreateNewCommunity(request);

				return CreatedAtAction(
					nameof(GetCommunity),
					new { communityId = newCommunity.Id },
					newCommunity
				);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error creating community.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("join-community")]
		public async Task<IActionResult> JoinCommunity([FromBody] CommunityJoinCommand request)
		{
			try
			{
				await communityService.AddUserToCommunity(request);

				return Ok();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error joining community.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("community/{communityId}")]
		public async Task<ActionResult<CommunityDTO>> GetCommunity([FromRoute] int communityId)
		{
			try
			{
				var community = await communityService.GetCommunity(new CommunityGetCommand(communityId));

				return Ok(community);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting community.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("communities")]
		public async Task<ActionResult<IEnumerable<CommunityDTO>>> GetCommunities()
		{
			try
			{
				var communities = await communityService.GetCommunities();

				return Ok(communities);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting communities.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpGet("communities/{userId}")]
		public async Task<ActionResult<IEnumerable<CommunityDTO>>> GetUserCommunities([FromRoute] int userId)
		{
			try
			{
				var communities = await communityService.GetUserCommunities(new CommunityFetchByUserQuery(userId));

				return Ok(communities);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting user communities.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{communityId}/feed")]
		public async Task<ActionResult<IEnumerable<FeedItemDTO>>> GetFeedContent([FromRoute] int communityId)
		{
			try
			{
				var feedItems = await communityService.GetCommunityFeed(new CommunityFeedQuery(communityId));

				return Ok(feedItems);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting feed content.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("search/communities")]
		public async Task<ActionResult<IEnumerable<CommunityDTO>>> SearchCommunities([FromQuery] CommunitySearchQuery query)
		{
			try
			{
				var communities = await communityService.SearchCommunities(query);

				return Ok(communities);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error searching communities.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("community/{communityId}/search")]
		public async Task<ActionResult<IEnumerable<FeedItemDTO>>> SearchCommunityContent([FromRoute] int communityId, [FromQuery] string query)
		{
			try
			{
				var feedItems = await communityService.SearchInCommunity(new CommunitySearchContentQuery(query, communityId));

				return Ok(feedItems);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error searching community content.");
				return StatusCode(500, ex.Message);
			}
		}
	}
}

