using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Commands
{
    public record CreateRoleCommand(
        int UserId,
        int CommunityId,
        RoleType RoleType
    )
    {
        public int UserId { get; init; } = UserId;
        public int CommunityId { get; init; } = CommunityId;
        public RoleType RoleType { get; init; } = RoleType;
    }
}
