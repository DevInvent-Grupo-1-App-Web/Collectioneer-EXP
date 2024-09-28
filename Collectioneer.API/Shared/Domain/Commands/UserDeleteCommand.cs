namespace Collectioneer.API.Shared.Domain.Commands
{
	public record UserDeleteCommand(
		string Username,
		string Password
	)
	{
		public string Password { get; init; } = Password;
		public string Username { get; init; } = Username;
	}
}
