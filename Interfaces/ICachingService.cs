using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboMajkel.Interfaces
{
    public interface ICachingService
    {
        T GetFromCache<T>(string key) where T : class;
        void SetCache<T>(string key, T value) where T : class;
        void SetCache<T>(string key, T value, DateTime duration) where T : class;
        bool IsCached(string key);
        public void RemoveCache(string key);
    }
}
