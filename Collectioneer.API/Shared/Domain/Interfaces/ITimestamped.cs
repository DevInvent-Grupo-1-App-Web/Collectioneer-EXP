using Collectioneer.API.Shared.Domain.Models.Entities;

namespace Collectioneer.API.Shared.Domain.Interfaces
{
	public interface ITimestamped
	{
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}