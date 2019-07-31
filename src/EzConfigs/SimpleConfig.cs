using System;
using System.Collections.Concurrent;

namespace EzConfigs
{
    public class SimpleConfig : ConcurrentDictionary<string, string>, ISimpleConfig
    {
        public SimpleConfig() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public void Save<T>(string key, object value)
        {
            throw new NotImplementedException();
        }

        public T TryGet<T>(string key, T defaultValue)
        {
            throw new NotImplementedException();
        }
    }
}