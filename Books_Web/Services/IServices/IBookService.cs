using Books_Web.Models;

namespace Books_Web.Services.IServices
{
    public interface IBookService
    {
        Task<T> GetAllAsync<T>();
        Task<T> CreateAsync<T>(Book book);
        Task<T> UpdateAsync<T>(Book book);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetAsync<T>(int id);
    }
}
