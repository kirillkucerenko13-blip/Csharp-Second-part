using MyLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
    }
}