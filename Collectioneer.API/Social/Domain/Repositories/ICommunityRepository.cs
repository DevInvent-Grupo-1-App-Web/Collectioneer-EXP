using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface ICommunityRepository : IBaseRepository<Community>
    {
        public Task<ICollection<Community>> Search(string searchTerm);
    }
}
