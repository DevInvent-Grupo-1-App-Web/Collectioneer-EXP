namespace Collectioneer.API.Shared.Domain.Queries;

public record GetMediaElementsQuery(
	int ElementId,
	string ElementType
)
{
	public int ElementId { get; set; } = ElementId;
	public string ElementType { get; set; } = ElementType;
}