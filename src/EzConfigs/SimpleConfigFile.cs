using System.Threading.Tasks;

namespace EzConfigs
{
    public class SimpleConfigFile : ISimpleConfigFile
    {
        private readonly ISimpleJsonFile _simpleJsonFile;

        public SimpleConfigFile(ISimpleJsonFile simpleJsonFile)
        {
            _simpleJsonFile = simpleJsonFile;
        }

        public Task<ISimpleConfig> ReadFile(string filePath, ISimpleConfig defaultValue)
        {
            return _simpleJsonFile.ReadFileAsSingle<ISimpleConfig>(filePath);
        }

        public Task SaveFile(string filePath, ISimpleConfig config)
        {
            return _simpleJsonFile.SaveFileAsSingle(filePath, config, true);
        }
    }
}