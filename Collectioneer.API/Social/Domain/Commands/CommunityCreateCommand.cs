namespace Collectioneer.API.Social.Domain.Commands
{
    public record CommunityCreateCommand
    (
        string Name,
        string Description,
        int UserId
    )
    {
        public string Name { get; init; } = Name;
        public string Description { get; init; } = Description;
        public int UserId { get; init; } = UserId;
    }
}
