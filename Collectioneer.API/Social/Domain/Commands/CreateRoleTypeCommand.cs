namespace Collectioneer.API.Social.Domain.Commands
{
    public class CreateRoleTypeCommand(string Name)
    {
        public string Name { get; init; } = Name;
    }
}
