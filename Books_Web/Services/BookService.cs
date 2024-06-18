using Books_Web.Models;
using Books_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Books_Web.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly string _bookApiBaseUrl;

        public BookService(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory)
        {
            _bookApiBaseUrl = configuration.GetValue<string>("ServiceUrls:BookAPI");
        }

        public Task<T> CreateAsync<T>(Book book)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.POST,
                Data = book,
                Url = $"{_bookApiBaseUrl}/api/createBook"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{_bookApiBaseUrl}/api/deleteBook/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_bookApiBaseUrl}/api/getBooks"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_bookApiBaseUrl}/api/getBookById/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(Book book)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = book,
                Url = $"{_bookApiBaseUrl}/api/updateBook/{book.Id}"
            });
        }
    }
}
