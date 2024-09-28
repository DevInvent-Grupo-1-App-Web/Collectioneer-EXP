using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
		public Task<ICollection<Post>> GetPosts(int communityId);
        public Task<ICollection<Post>> Search(string searchTerm, int communityId); 
    }
}
