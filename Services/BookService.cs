using MyLibrary.Interfaces;
using MyLibrary.Models;
using PracticalWork5.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new Exception("Book title is empty");

            await _repository.AddAsync(book);
        }

        public Task<List<Book>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}