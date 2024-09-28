using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleRegisterIntoUserCommand
		(
			Collectible Collectible
		)
    {
        public Collectible Collectible { get; init; } = Collectible;
    }
}
