using System.Text.RegularExpressions;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Application.Internal.Services;

public class MediaElementService(
	AppKeys appKeys,
	IMediaElementRepository mediaElementRepository,
	IUnitOfWork unitOfWork
	) : IMediaElementService
{
	private readonly BlobServiceClient _blobServiceClient = new BlobServiceClient(appKeys.BlobStorage.URL);

	public async Task<ICollection<MediaElement>> GetMediaElementsByCollectibleId(int collectibleId)
	{
		return await mediaElementRepository.GetMediaElementsByCollectibleId(collectibleId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByCommunityId(int communityId)
	{
		return mediaElementRepository.GetMediaElementsByCommunityId(communityId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByPostId(int postId)
	{
		return mediaElementRepository.GetMediaElementsByPostId(postId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByProfileId(int profileId)
	{
		return mediaElementRepository.GetMediaElementsByProfileId(profileId);
	}

	public async Task<string> UploadMedia(MediaElementUploadCommand request, int uploaderId)
	{
		// Get a reference to a blob container
		var blobContainerClient = _blobServiceClient.GetBlobContainerClient("media");

		// Get a reference to a blob
		var blobClient = blobContainerClient.GetBlobClient($"{uploaderId}/{request.MediaName}");

		// Upload the file to the blob
		using var stream = new MemoryStream(Convert.FromBase64String(request.Content), writable: false);
		await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = request.ContentType });

		string url = blobClient.Uri.ToString();

		Match match = Regex.Match(url, @"^(.*\/[^?]*)");
		if (match.Success)
		{
			url = match.Groups[1].Value;
		}

		// Create a new media element
		var mediaElement = new MediaElement
		(uploaderId, request.MediaName, url);

		switch (request.AttachedToType)
		{
			case "collectible":
				mediaElement.CollectibleId = request.AttachedToId;
				break;
			case "community":
				mediaElement.CommunityId = request.AttachedToId;
				break;
			case "post":
				mediaElement.PostId = request.AttachedToId;
				break;
			case "profile":
				mediaElement.ProfileId = request.AttachedToId;
				break;

			default:
				throw new ArgumentException("Invalid attached to type");
		}

		// Save the media element to the database
		await mediaElementRepository.Add(mediaElement);
		await unitOfWork.CompleteAsync();

		return url;
	}

	public Task MarkMediaElementAsDeleted(int uploaderId, string mediaName)
	{
		throw new NotImplementedException();
	}

	public async Task MarkMediaElementAsModerated(int uploaderId, string mediaName)
	{
		var mediaElement = await mediaElementRepository.GetMediaElementByUploaderIdAndMediaName(uploaderId, mediaName);
		mediaElement.Moderated = true;
		await unitOfWork.CompleteAsync();
	}
	
	public Task MarkMediaElementAsHidden(int uploaderId, string mediaName)
	{
		throw new NotImplementedException();
	}
}
