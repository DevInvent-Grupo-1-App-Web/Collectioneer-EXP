using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services
{
    public class RoleService(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork
    ) : IRoleService
    {
        public async Task CreateNewRole(CreateRoleCommand command)
        {
            var newRole = new Role(command.UserId, command.CommunityId, 1);

            try
            {
                await roleRepository.Add(newRole);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error creating role.", ex);
            }
        }

		public async Task<ICollection<Role>> GetUserRoles(int userId)
		{
			var roles = await roleRepository.GetRolesByUserId(userId);
			return roles;
		}
    }
}
