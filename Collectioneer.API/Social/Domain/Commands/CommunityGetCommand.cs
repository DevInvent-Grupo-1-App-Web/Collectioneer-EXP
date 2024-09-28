namespace Collectioneer.API.Social.Domain.Commands
{
    public record CommunityGetCommand(  
        int Id
    )
    {
        public int Id { get; init; } = Id;
    }
}
