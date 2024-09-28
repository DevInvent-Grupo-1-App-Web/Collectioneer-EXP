using Collectioneer.API.Shared.Application.Exceptions;

namespace Collectioneer.API.Shared.Infrastructure.Exceptions
{
    public class EntityNotFoundException(string message) : ExposableException(message, 404)
    {
    }
}
