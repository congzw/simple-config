using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EzConfigs
{
    public class SimpleConfig : ConcurrentDictionary<string, object>, ISimpleConfig
    {
        private readonly object _lock = new object();

        public SimpleConfig() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public void AddOrUpdate<T>(string key, T value)
        {
            lock (_lock)
            {
                this[key] = value;
            }
        }

        public T TryGet<T>(string key, T defaultValue)
        {
            lock (_lock)
            {
                if (!this.ContainsKey(key))
                {
                    return defaultValue;
                }
                return (T)this[key];
            }
        }
    }
}