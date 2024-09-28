namespace Collectioneer.API.Shared.Application.Exceptions
{
    public class DuplicatedCredentialsException(string message) : ExposableException(message, 400)
    {
    }
}
