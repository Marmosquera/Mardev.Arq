using Mardev.Arq.Shared.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mardev.Arq.Front.Web.Services
{
    public abstract class BaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly string _serviceName;

        public BaseService(IHttpClientFactory httpClientFactory, string serviceName)
        {
            _httpClientFactory = httpClientFactory;
            _serviceName = serviceName;
        }

        public async Task<ServiceResponse<T>> SendAsync<T>(ServiceRequest request, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient(_serviceName);
                HttpRequestMessage message = new();
                if (request.ContentType == ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }
                //token
                if (withBearer)
                {
                    //var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {request.AccessToken}");
                }

                message.RequestUri = new Uri(request.Url);

                if (request.ContentType == ContentType.MultipartFormData && request.Data != null)
                {
                    var content = new MultipartFormDataContent();

                    foreach (var prop in request.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(request.Data);
                        if (value is FormFile)
                        {
                            var file = (FormFile)value;
                            if (file != null)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else
                        {
                            content.Add(new StringContent($"{value}"), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else
                {
                    if (request.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                    }
                }


                HttpResponseMessage? apiResponse = null;

                switch (request.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseData = JsonConvert.DeserializeObject<T>(apiContent);
                        return new ServiceResponse<T>
                        {
                            Result = apiResponseData,
                            IsSuccess = true
                        };
                }
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse<T>
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return response;
            }
        }


    }
}
