using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
    [ApiController]
    public class AuctionController(IAuctionService auctionService, ILogger<AuctionController> logger) : ControllerBase
    {
		[HttpGet("auctions")]
		public async Task<ActionResult<ICollection<AuctionDTO>>> GetAuctions([FromQuery]AuctionBulkRetrieveQuery query)
        {
            try
            {
                var response = await auctionService.GetAuctions(query);
                return Ok(response);
            }
			catch (ExposableException ex)
			{
				logger.LogError(ex, "Error getting auctions.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting auctions.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("auction")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionFromCollectible([FromQuery] AuctionGetByCollectibleIdQuery query)
        {
            try
            {
                var response = await auctionService.GetAuctionFromCollectibleId(query);
                return Ok(response);
            }
			catch (ExposableException ex)
			{
				logger.LogError(ex, "Error getting auction.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting auction.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("auctions/{id}")]
        public async Task<IActionResult> GetAuction([FromRoute] int id)
        {
            try
            {
                var response = await auctionService.GetAuction(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting auction.");
                return StatusCode(500, ex.Message);
            }
        }

		[Authorize]
		[HttpPost("auctions")]
		public async Task<ActionResult<AuctionDTO>> CreateAuction([FromBody] AuctionCreationCommand command)
		{
			try
			{
				var response = await auctionService.CreateAuction(command);
				return CreatedAtAction(
					nameof(GetAuction), 
					new { id = response.Id }, 
					response
				);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error creating auction.");
				return StatusCode(500, ex.Message);
			}
		}

        [Authorize]
        [HttpPost("auctions/bids")]
        public async Task<IActionResult> CreateBid([FromBody] BidCreationCommand command)
        {
            try
            {
                var response = await auctionService.PlaceBid(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating bid.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("auctions/bids")]
        public async Task<IActionResult> GetBids([FromQuery] BidRetrieveQuery query)
        {
            try
            {
                var response = await auctionService.GetBids(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting bids.");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("auctions/close")]
        public async Task<IActionResult> CloseAuction([FromBody] AuctionCloseCommand command)
        {
            try
            {
                var response = await auctionService.CloseAuction(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error closing auction.");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("auctions/confirm")]
        public async Task<IActionResult> AuctioneerConfirmation([FromBody] AuctionValidationCommand command)
        {
            try
            {
                if (command.AsAuctioneer)
                {
                    await auctionService.AuctioneerConfirmation(command);
                    return Ok();
                }
                else if (command.AsBidder)
                {
                    await auctionService.BidderConfirmation(command);
                    return Ok();
                }
                else
                {
                    return BadRequest("Operation role not specified.");
                }
            }
			catch (ExposableException ex)
			{
				logger.LogError(ex, "Error confirming auctioneer.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
            catch (Exception ex)
            {
                logger.LogError(ex, "Error confirming auctioneer.");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
