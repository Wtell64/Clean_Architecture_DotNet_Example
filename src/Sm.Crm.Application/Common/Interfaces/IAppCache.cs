namespace Sm.Crm.Application.Common.Interfaces;

public interface IAppCache
{
    Task SetCache(string key, string value, int timeout = 1);
    Task<string?> GetCache(string key);
    Task<T?> GetOrSetCache<T>(string key, Func<T> action, int timeout = 1);
    Task RemoveCache(string key);
}