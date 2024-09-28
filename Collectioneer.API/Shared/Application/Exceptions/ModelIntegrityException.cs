namespace Collectioneer.API.Shared.Application.Exceptions
{
	public class ModelIntegrityException(string message) : ExposableException(message, 409)
	{
	}
}