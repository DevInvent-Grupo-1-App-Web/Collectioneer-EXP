namespace Collectioneer.API.Shared.Domain.Commands
{
	public record PasswordChangeCommand(
		string Username,
		string NewPassword,
		string RecoveryToken
	)
	{
		public string Username { get; init; } = Username;
		public string NewPassword { get; init; } = NewPassword;
		public string RecoveryToken { get; init; } = RecoveryToken;
	}
}