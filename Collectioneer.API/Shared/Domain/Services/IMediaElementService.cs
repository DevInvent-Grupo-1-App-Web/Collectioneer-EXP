using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Models.Entities;

namespace Collectioneer.API.Shared.Domain.Services
{
	public interface IMediaElementService
	{
		Task<string> UploadMedia(MediaElementUploadCommand request, int uploaderId);
		Task<ICollection<MediaElement>> GetMediaElementsByCollectibleId(int collectibleId);

		Task<ICollection<MediaElement>> GetMediaElementsByCommunityId(int communityId);

		Task<ICollection<MediaElement>> GetMediaElementsByPostId(int postId);

		Task<ICollection<MediaElement>> GetMediaElementsByProfileId(int profileId);

		Task MarkMediaElementAsModerated(int uploaderId, string mediaName);
		Task MarkMediaElementAsDeleted(int uploaderId, string mediaName);
		Task MarkMediaElementAsHidden(int uploaderId, string mediaName);
	}
}