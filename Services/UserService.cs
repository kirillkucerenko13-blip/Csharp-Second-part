using MyLibrary.Interfaces;
using MyLibrary.Models;
using PracticalWork5.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new Exception("User name is empty");

            await _repository.AddAsync(user);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}