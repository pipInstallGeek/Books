using Books_Web.Controllers;
using Books_Web.Models;
using Books_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Books_Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BookAPI");
                var request = new HttpRequestMessage
                {
                    Headers = { { "Accept", "application/json" } },
                    RequestUri = new Uri(apiRequest.Url)
                };

                // Add request body if applicable
                if (apiRequest.Data != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                // Set HTTP method
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        request.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        request.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        request.Method = HttpMethod.Delete;
                        break;
                    default:
                        request.Method = HttpMethod.Get;
                        break;
                }

                // Send request and get response
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON content to the requested type T
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new ApplicationException("Error in SendAsync method", ex);
            }
        }
    }
}
