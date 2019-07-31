namespace EzConfigs
{
    public interface ISimpleConfig
    {
        void Save<T>(string key, object value);
        T TryGet<T>(string key, T defaultValue);
    }
}
