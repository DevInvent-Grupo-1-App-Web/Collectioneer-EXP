using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
    {
		public async Task<ICollection<Role>> GetRolesByUserId(int userId)
		{
			return await _context.Roles.Where(r => r.UserId == userId).ToListAsync();
		}
	}
}
