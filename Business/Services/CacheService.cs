
using System.Runtime.Caching;

namespace Business.Services;

public class CacheService : ICacheService
{
    private ObjectCache _memoryCache = MemoryCache.Default;
    public T GetData<T>(string key)
    {
        try
        {
            T item = (T)_memoryCache.Get(key);
            return item;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public object RemoveData(string key)
    {
        var result = true;
        try
        {
            if (!string.IsNullOrEmpty(key))
            {
                var res = _memoryCache.Remove(key);
            }
            else
                result = false;

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var result = true;
        try
        {
            if (!string.IsNullOrEmpty(key))
                _memoryCache.Set(key, value, expirationTime);   
            else
                result = false;

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
