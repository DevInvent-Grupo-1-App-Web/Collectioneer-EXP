using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Application.External.Objects;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[ApiController]
	public class UserController(
		IContentModerationService contentModerationService,
		IUserService userService,
		ILogger<UserController> logger) : ControllerBase
	{
		[HttpGet("user/{id}")]
		[ProducesResponseType(typeof(UserDTO), 200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<ActionResult<UserDTO>> GetUser([FromRoute] int id)
		{
			try
			{
				var response = await userService.GetUser(id);
				return StatusCode(200, response);
			}
			catch (ExposableException ex)
			{
				logger.LogInformation(ex, "Error getting user.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error getting user.");
				return StatusCode(500, ex.Message);
			}
		}


		[HttpPost("register-user")]
		[ProducesResponseType(typeof((UserDTO, string, string)), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommand request)
		{
			try
			{
				if (!await contentModerationService.ScreenTextContent($"{request.Name} {request.Username}"))
				{
					throw new ExposableException("Review your credentials properness.", 400);
				}

				var registerResponse = await userService.RegisterNewUser(request);
				var loginRequest = new UserLoginQuery(request.Username, request.Password);
				var loginResponse = await userService.LoginUser(loginRequest);

				return Ok(new { user = registerResponse, token = loginResponse, type = "Bearer" });
			}
			catch (ExposableException ex)
			{
				logger.LogInformation(ex, "Error registering user.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error registering user.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof((string, string, int)), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> LoginUser([FromBody] UserLoginQuery request)
		{
			try
			{
				var response = await userService.LoginUser(request);
				var requestedUser = await userService.GetUserByUsername(request.Username);
				var userId = requestedUser.Id;
				return Ok(new { token = response, type = "Bearer", userId });
			}
			catch (ExposableException ex)
			{
				logger.LogInformation(ex, "Error logging in user.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error logging in user.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpDelete("delete-user")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> DeleteUser([FromBody] UserDeleteCommand request)
		{
			try
			{
				await userService.DeleteUser(request);
				return Ok();
			}
			catch (ExposableException ex)
			{
				logger.LogInformation(ex, "Error deleting user.");
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error deleting user.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("forgot-password")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
		{
			try
			{
				await userService.ForgotPassword(request);
				return Ok();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error sending password reset email.");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("change-password")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeCommand request)
		{
			try
			{
				await userService.ChangeUserPassword(request);
				return Ok();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error changing password.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpGet("who-is")]
		public async Task<int> WhoIs([FromQuery] string token)
		{
			return await userService.GetUserIdByToken(token);
		}
	}
}
