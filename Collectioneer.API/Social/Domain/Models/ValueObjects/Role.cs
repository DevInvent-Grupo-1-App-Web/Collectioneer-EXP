using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects;

public class Role(
	int userId,
	int communityId,
	int roleTypeId
	) : ITimestamped
{
    public int Id { get; set; }
	public int UserId { get; set; } = userId;
	public int CommunityId { get; set; } = communityId;
	public int RoleTypeId { get; set; } = roleTypeId;
	public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Community? Community { get; set; }
    public RoleType? RoleType { get; set; }
}
