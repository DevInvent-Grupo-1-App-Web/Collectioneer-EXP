namespace Collectioneer.API.Shared.Domain.Queries
{
    public record UserLoginQuery
		(
			string Username,
			string Password
		)
    {
        public string Username { get; init; } = Username;
        public string Password { get; init; } = Password;
    }
}
