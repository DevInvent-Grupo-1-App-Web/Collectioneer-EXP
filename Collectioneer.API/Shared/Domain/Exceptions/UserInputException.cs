using Collectioneer.API.Shared.Application.Exceptions;

namespace Collectioneer.API.Shared.Domain.Exceptions
{
	public class UserInputException(string message) : ExposableException(message, 400)
	{
	}
}
