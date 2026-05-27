using MyLibrary.Interfaces;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Order order)
        {
            if (order.UserId <= 0 || order.BookId <= 0)
                throw new Exception("Invalid order data");

            await _repository.AddAsync(order);
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}