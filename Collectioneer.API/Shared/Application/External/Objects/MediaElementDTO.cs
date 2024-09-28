namespace Collectioneer.API.Shared.Application.External.Objects
{
    public record MediaElementDTO(
        int Id,
        int UploaderId,
        string MediaName,
        string MediaURL,
        string CreatedAt,
        string UpdatedAt
    )
    {
        public int Id { get; init; } = Id;
        public int UploaderId { get; init; } = UploaderId;
        public string MediaName { get; init; } = MediaName;
        public string MediaURL { get; init; } = MediaURL;
        public string CreatedAt { get; init; } = CreatedAt;
        public string UpdatedAt { get; init; } = UpdatedAt;
    }
}
