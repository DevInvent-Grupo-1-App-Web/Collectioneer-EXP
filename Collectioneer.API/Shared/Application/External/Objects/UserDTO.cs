using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Shared.Application.External.Objects
{
	public record UserDTO
	{
		public int Id { get; init; }
		public string Username { get; init; }
		public string Email { get; init; }
		public string Name { get; init; }

		public UserDTO(User user)
		{
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Name = user.Name;
        }
	}
}
