using Collectioneer.API.Social.Application.External;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Queries;

namespace Collectioneer.API.Social.Domain.Services {
	public interface IPostService {
		public Task<PostDTO> AddPost(AddPostCommand command);
		public Task<ICollection<PostDTO>> Search(PostSearchQuery query);
		public Task<PostDTO?> GetPost(int postId);

    }
}