using Collectioneer.API.Shared.Domain.Models.Entities;

namespace Collectioneer.API.Shared.Domain.Interfaces
{
	public interface IMediaHolder
	{
		public ICollection<MediaElement> MediaElements { get; set; }
	}
}