namespace Collectioneer.API.Shared.Domain.Commands
{
    public record UserRegisterCommand
		(
			string Email,
			string Name,
			string Password,
			string Username
		
		)
    {
        public string Email { get; init; } = Email;
        public string Name { get; init; } = Name;
        public string Password { get; init; } = Password;
        public string Username { get; init; } = Username;
    }
}
