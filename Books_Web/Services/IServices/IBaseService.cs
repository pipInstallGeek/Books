using AutoMapper.Internal;
using Books_Web.Models;

namespace Books_Web.Services.IServices
{
    public interface IBaseService
    {
        //APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
