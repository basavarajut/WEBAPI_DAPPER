using WEBAPI_DAPPER.Models.Domain;

namespace WEBAPI_DAPPER.Interfaces
{
    public interface IDataService<T> 
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllById(int id);
        Task<T> PostContact(T entity);
        Task<T> UpdateContact(T entity);
        Task<bool> DeleteContact(Guid id);
    }
}
