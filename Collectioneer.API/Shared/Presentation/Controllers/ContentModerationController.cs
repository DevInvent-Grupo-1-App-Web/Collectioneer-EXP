using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[ApiController]
	public class ContentModerationController(
		IContentModerationService contentModerationService,
		IMediaElementService mediaElementService,
		ILogger<ContentModerationController> logger,
		AppKeys appKeys
		) : ControllerBase
	{

		[HttpPost("text")]
		public async Task<IActionResult> IsTextContentSafe([FromBody] string content)
		{
			try
			{
				var result = await contentModerationService.ScreenTextContent(content);
				return Ok(result);
			}
			catch (Microsoft.Azure.CognitiveServices.ContentModerator.Models.APIErrorException apiEx)
			{
				logger.LogError(apiEx, "API error while checking text content safety: {Message}", apiEx.Message);
				return BadRequest(new { error = "Content moderation API error", details = apiEx.Message });
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error while checking text content safety.");
				return StatusCode(500, "Internal server error.");
			}
		}

		/// <summary>
		/// Receives an API request from the image moderation service and marks the image as moderated.
		/// </summary>
		/// <param name="imageUrl"></param>
		/// <returns></returns>
		[HttpPost("image")]
		public async Task<IActionResult> SetModerationResult([FromBody]MediaElementModerationCommand command)
		{
			if (command.MediaModeratorKey != appKeys.ContentSafety.ClientServiceKey)
			{
				return Unauthorized();
			}

			try
			{
				await mediaElementService.MarkMediaElementAsModerated(command.UploaderId, command.MediaName);
				return Ok();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error while marking media element as moderated.");
				return StatusCode(500, "Internal server error.");
			}
		}
	}
}