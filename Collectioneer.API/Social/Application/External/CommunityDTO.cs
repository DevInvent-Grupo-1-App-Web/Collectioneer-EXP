using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Application.External
{
    public class CommunityDTO(Community community)
	{
		public int Id { get; init; } = community.Id;
		public string Name { get; init; } = community.Name;
		public string Description { get; init; } = community.Description;
	}
}
