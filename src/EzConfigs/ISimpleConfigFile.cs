using System.Threading.Tasks;

namespace EzConfigs
{
    public interface ISimpleConfigFile
    {
        Task<ISimpleConfig> ReadFile(string filePath, ISimpleConfig defaultValue);
        Task SaveFile(string filePath, ISimpleConfig config);
    }
}