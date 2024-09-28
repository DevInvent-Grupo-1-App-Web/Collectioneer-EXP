namespace Collectioneer.API.Shared.Domain.Services.Communication
{
    public abstract class BaseResponse<T>
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T? Resource { get; protected set; }
        protected BaseResponse(T resource)
        {
            Resource = resource;
            Success = true;
            Message = string.Empty;
        }
        protected BaseResponse(string message)
        {
            Message = message;
            Success = false;
            Resource = default;
        }
    }
}