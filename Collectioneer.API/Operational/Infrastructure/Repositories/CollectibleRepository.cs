using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Collectioneer.API.Operational.Infrastructure.Repositories
{
	public class CollectibleRepository(AppDbContext context) : BaseRepository<Collectible>(context), ICollectibleRepository
	{
		public async Task DeleteUserCollectibles(int userId)
		{
			var collectibles = await _context.Collectibles.Where(c => c.OwnerId == userId).ToListAsync();

			foreach (var collectible in collectibles)
			{
				if (collectible.IsLinkedToProcess())
				{
					throw new ModelIntegrityException("Collectible is currently in active process. Cannot delete.");
				}
				_context.Collectibles.Remove(collectible);
			}

			await _context.SaveChangesAsync();
		}

		public async Task<ICollection<Collectible>> GetCollectibles(int communityId, int maxAmount = -1, int offset = 0)
		{
			var query = _context.Collectibles
			.Include(c => c.MediaElements)
			.Include(c => c.Owner)
			.Include(c => c.Community)
			.Where(c => c.CommunityId == communityId);

			if (offset > 0)
			{
				query = query.Skip(offset);
			}

			if (maxAmount != -1)
			{
				query = query.Take(maxAmount);
			}

            return await query.ToListAsync();
        }

		public async Task<ICollection<Collectible>> Search(string searchTerm, int communityId)
		{
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

            var collectibles = await _context.Collectibles.Include(c => c.MediaElements)
			.Include(c => c.Owner)
			.Include(c => c.Community).Where(c => c.CommunityId == communityId && (c.Name.Contains(searchTerm) || c.Description.Contains(searchTerm)))
                .ToListAsync();

			return collectibles;
		}
	}
}
