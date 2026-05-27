using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        void Add(T item);
        Task AddAsync(T item);
    }
}