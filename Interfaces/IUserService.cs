using MyLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}