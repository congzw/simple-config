using System;

namespace EzConfigs
{
    public class SimpleConfigFactory
    {
        #region for di extensions

        private static readonly SimpleConfig Instance = new SimpleConfig();
        public static Func<ISimpleConfig> Resolve { get; set; } = () => Instance;

        #endregion
    }
}