using System.Text.RegularExpressions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class CommunityRepository(AppDbContext context) : BaseRepository<Community>(context), ICommunityRepository
    {
		public async Task<ICollection<Community>> Search(string searchTerm)
        {
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

            var communities = await this._context.Communities.Where(c => c.Name.Contains(searchTerm)).ToListAsync();

            return communities;
        }
    }
}
