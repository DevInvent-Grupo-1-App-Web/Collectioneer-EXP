using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IRoleService
    {
        public Task CreateNewRole(CreateRoleCommand command);
		public Task<ICollection<Role>> GetUserRoles(int userId);
    }
}
