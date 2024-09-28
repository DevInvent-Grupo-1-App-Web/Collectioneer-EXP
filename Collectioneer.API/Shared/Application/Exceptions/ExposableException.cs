namespace Collectioneer.API.Shared.Application.Exceptions
{
    public partial class ExposableException(string message, int statusCode, Exception? innerException = null) : Exception(message, innerException)
    {
		public int StatusCode { get; set; } = statusCode;
	}
}