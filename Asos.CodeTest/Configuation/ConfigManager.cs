using System;
using System.Configuration;

namespace Asos.CodeTest.Configuation
{
    public class ConfigManager : IConfigManager
    {

        private const string FailOverMode = "IsFailoverModeEnabled";
        public bool IsFailoverModeEnabled
        {
            get
            {
                return ConfigurationManager.AppSettings[FailOverMode] != null && Convert.ToBoolean(ConfigurationManager.AppSettings[FailOverMode]);
            }
        }
    }
}
