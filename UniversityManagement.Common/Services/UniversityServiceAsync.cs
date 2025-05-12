using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityManagement.Common.Services
{
    public class UniversityServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly List<T> _items = new List<T>();
        private readonly object _lock = new object();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly string _filePath;

        public UniversityServiceAsync(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<bool> CreateAsync(T element)
        {
            lock (_lock)
            {
                _items.Add(element);
            }
            return await Task.FromResult(true);
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    var prop = typeof(T).GetProperty("Id");
                    return _items.FirstOrDefault(x => prop != null && prop.GetValue(x).Equals(id));
                }
            });
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.ToList();
                }
            });
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.Skip((page - 1) * amount).Take(amount).ToList();
                }
            });
        }

        public async Task<bool> UpdateAsync(T element)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    var prop = typeof(T).GetProperty("Id");
                    var id = prop.GetValue(element);
                    var index = _items.FindIndex(x => prop.GetValue(x).Equals(id));
                    if (index >= 0)
                    {
                        _items[index] = element;
                        return true;
                    }
                    return false;
                }
            });
        }

        public async Task<bool> RemoveAsync(T element)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.Remove(element);
                }
            });
        }

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonConvert.SerializeObject(_items, Newtonsoft.Json.Formatting.Indented);
                await Task.Run(() => File.WriteAllText(_filePath, json));
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _items.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
