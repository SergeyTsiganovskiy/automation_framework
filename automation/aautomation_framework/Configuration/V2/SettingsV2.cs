using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aautomation_framework.Utility;
using aautomation_framework.Utility.Helpers;

namespace aautomation_framework.Configuration.V2
{
    public class SettingsV2
    {
        private static SettingsV2 instance;
        public MCCommonConfigurationsVariablesModel mcCommonVariablesModel = DeserializeJsonHelper.DeserializeJson<MCCommonConfigurationsVariablesModel>(
            @"Configuration\MCCommonVariables.json");
        private Lazy<string> EnvName = new Lazy<string>(() => instance.mcCommonVariablesModel.McActiveConfiguration?.Name);
        private string testStack => Environment.GetEnvironmentVariable("TestStack") ?? EnvName.Value;

        public static SettingsV2 Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettingsV2();
                return instance;
            }
        }

        private SettingsV2()
        {
        }

        public string GetEnvUrl(string envName = null)
        {
            List<SystemConfiguration> commonSysConfigs = CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj;
            if (envName == null)
            {
                envName = testStack;
            }

            foreach (var config in mcCommonVariablesModel.CommonSystemConfigurations.Concat(commonSysConfigs))
            {
                if (envName == config.Name)
                {
                    return config.Url;
                }
            }

            LogUtil.WriteDebug($"Url navigating to {envName} was not found in ");
            return null;
        }
    }
}
