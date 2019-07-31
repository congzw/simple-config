namespace EzConfigs
{
    public interface ISimpleConfigFile
    {
        ISimpleConfig ReadFile(string filePath);
        void SaveFile(string filePath, ISimpleConfig config);
    }
}