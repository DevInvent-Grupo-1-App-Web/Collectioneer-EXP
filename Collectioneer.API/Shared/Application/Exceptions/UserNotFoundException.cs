namespace Collectioneer.API.Shared.Application.Exceptions
{
    public class UserNotFoundException(string message) : ExposableException(message, 404)
    {
    }
}
