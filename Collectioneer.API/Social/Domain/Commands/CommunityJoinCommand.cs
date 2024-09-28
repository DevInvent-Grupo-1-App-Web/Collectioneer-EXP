namespace Collectioneer.API.Social.Domain.Commands
{
    public class CommunityJoinCommand
        (
        int UserId,
        int CommunityId
        )
    {
        public int UserId { get; set; } = UserId;
        public int CommunityId { get; set; } = CommunityId;
    }
}
