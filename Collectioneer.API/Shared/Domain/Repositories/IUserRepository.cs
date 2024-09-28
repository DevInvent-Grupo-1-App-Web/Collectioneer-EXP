using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Shared.Domain.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		public Task<bool> IsEmailUnique(string email);

		public Task<bool> IsUsernameUnique(string username);

		public Task<bool> IsValidUser(string username, string password);

		public Task<User?> GetUserByUsername(string username);
		public Task<User?> GetUserByEmail(string email);
	}
}
