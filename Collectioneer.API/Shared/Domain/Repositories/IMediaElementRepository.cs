using Collectioneer.API.Shared.Domain.Models.Entities;

namespace Collectioneer.API.Shared.Domain.Repositories;

public interface IMediaElementRepository : IBaseRepository<MediaElement>
{
	Task<ICollection<MediaElement>> GetMediaElementsByCollectibleId(int collectibleId);
	Task<ICollection<MediaElement>> GetMediaElementsByPostId(int postId);
	Task<ICollection<MediaElement>> GetMediaElementsByProfileId(int profileId);
	Task<ICollection<MediaElement>> GetMediaElementsByCommunityId(int communityId);
	Task<MediaElement> GetMediaElementByUploaderIdAndMediaName(int uploaderId, string mediaName);
}