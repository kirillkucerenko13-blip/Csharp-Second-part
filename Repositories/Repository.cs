using MyLibrary.Interfaces;
using MyLibrary.Utils;
using PracticalWork5;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private readonly string _filePath;
        private readonly JsonSerializerHelper _serializer;

        public Repository(string filePath)
        {
            _filePath = filePath;
            _serializer = new JsonSerializerHelper();
        }

        public List<T> GetAll()
        {
            return _serializer.Load<List<T>>(_filePath) ?? new List<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _serializer.LoadAsync<List<T>>(_filePath) ?? new List<T>();
        }

        public void Add(T item)
        {
            var data = GetAll();
            data.Add(item);
            _serializer.Save(_filePath, data);
        }

        public async Task AddAsync(T item)
        {
            var data = await GetAllAsync();
            data.Add(item);
            await _serializer.SaveAsync(_filePath, data);
        }
    }
}