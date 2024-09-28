using System.Text.RegularExpressions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class PostRepository(AppDbContext context) : BaseRepository<Post>(context), IPostRepository
    {
		public async Task<ICollection<Post>> GetPosts(int communityId)
		{
			return await _context.Posts
				.Include(p => p.Author)
				.Include(p => p.MediaElements)
				.Include(p => p.Community)
				.Where(p => p.CommunityId == communityId)
				.ToListAsync();
		}

		public async Task<ICollection<Post>> Search(string searchTerm, int communityId)
		{
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

            var posts = await _context.Posts.Include(p => p.Author)
				.Include(p => p.MediaElements)
				.Include(p => p.Community)
                .Where(p => p.CommunityId == communityId && (p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm)))
                .ToListAsync();

			return posts;
		}
	}
}