using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyLibrary.Utils
{
    public class JsonSerializerHelper
    {
        public void Save<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }

        public async Task SaveAsync<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(path, json);
        }

        public T Load<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task<T> LoadAsync<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}