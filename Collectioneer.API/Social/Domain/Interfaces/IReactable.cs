using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Interfaces
{
	public interface IReactable
	{
		public ICollection<Reaction> Reactions { get; set; }
	}
}