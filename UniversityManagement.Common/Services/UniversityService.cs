using System.Text.Json;

namespace UniversityManagement.Common.Services;

public class UniversityService<T> : ICrudService<T> where T : class
{
    private List<T> _items = new();

    public void Create(T element) => _items.Add(element);

    public T Read(Guid id)
    {
        var prop = typeof(T).GetProperty("Id");
        return _items.FirstOrDefault(x => prop?.GetValue(x)?.Equals(id) == true);
    }

    public IEnumerable<T> ReadAll() => _items;

    public void Update(T element)
    {
        var prop = typeof(T).GetProperty("Id");
        var id = prop?.GetValue(element);
        var index = _items.FindIndex(x => prop?.GetValue(x)?.Equals(id) == true);
        if (index >= 0) _items[index] = element;
    }

    public void Remove(T element) => _items.Remove(element);

    public void Save(string path)
    {
        var json = JsonSerializer.Serialize(_items);
        File.WriteAllText(path, json);
    }

    public void Load(string path)
    {
        if (!File.Exists(path)) return;
        var json = File.ReadAllText(path);
        var loaded = JsonSerializer.Deserialize<List<T>>(json);
        if (loaded != null) _items = loaded;
    }
}