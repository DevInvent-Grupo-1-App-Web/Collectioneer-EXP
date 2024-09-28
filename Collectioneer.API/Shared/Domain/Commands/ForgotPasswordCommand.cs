namespace Collectioneer.API.Shared.Domain.Commands
{
    public record ForgotPasswordCommand
		(
			string Email
		)
    {
        public string Email { get; init; } = Email;
    }
}