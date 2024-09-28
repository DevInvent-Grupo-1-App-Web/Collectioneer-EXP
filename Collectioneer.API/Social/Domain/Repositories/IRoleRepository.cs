using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
		public Task<ICollection<Role>> GetRolesByUserId(int userId);
    }

}
