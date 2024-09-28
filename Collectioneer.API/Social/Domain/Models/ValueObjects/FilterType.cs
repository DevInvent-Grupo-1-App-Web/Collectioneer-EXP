namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class FilterType(
		string name
		)
	{
		public int Id { get; set; }
		public string Name { get; set; } = name;
		// Navigation properties
		public ICollection<Filter> Filters { get; set; } = [];
	}
}