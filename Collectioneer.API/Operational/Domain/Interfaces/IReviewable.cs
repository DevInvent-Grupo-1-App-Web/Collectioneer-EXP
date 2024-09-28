using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Operational.Domain.Interfaces
{
		public interface IReviewable
		{
				public ICollection<Review> Reviews { get; set; }
		}
}