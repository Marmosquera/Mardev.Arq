namespace Mardev.Arq.Shared.Contracts
{
    public class ServiceRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum ContentType
    {
        Json,
        MultipartFormData,
    }
}
