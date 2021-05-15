using Microsoft.Extensions.Caching.Memory;
using RoboMajkel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboMajkel
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _cache;
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }
        private readonly DateTime defualtDuration = DateTime.Now.AddDays(1);
        public T GetFromCache<T>(string key) where T : class
        {
            _cache.TryGetValue(key, out T result);
            return result;
        }
        public void SetCache<T>(string key, T value) where T : class 
        {
            _cache.Set(key, value, defualtDuration);
        }
        public void SetCache<T>(string key, T value, DateTime duration) where T : class
        {
            _cache.Set(key, value, duration);
        }
        public bool IsCached(string key)
        {
            bool isCached =_cache.TryGetValue(key, out object o);
            return isCached;
        }
        public void RemoveCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
