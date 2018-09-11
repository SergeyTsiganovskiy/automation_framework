using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace aautomation_framework.Configuration.V2
{
    public class CommonTestingConfiguration
    {
        public CommonVariables commonVariables = null;

        #region singleton
        private static CommonTestingConfiguration instance;

        public static CommonTestingConfiguration Instance
        {
            get
            {
                if (instance == null)
                    instance = new CommonTestingConfiguration();
                return instance;
            }
        }
        private CommonTestingConfiguration()
        {
            using (StreamReader r = new StreamReader(TestContext.CurrentContext.TestDirectory + @"/../../../ExaLinkCommonLib/Config/CommonVariables.json"))
            {
                string json = r.ReadToEnd();
                commonVariables = JsonConvert.DeserializeObject<CommonVariables>(json);
            }
        }

        #endregion singleton
    }

    #region json_class_common_config

    public class SystemConfiguration
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("URL")]
        public string Url { get; set; }
        [JsonProperty("ApiPort")]
        public string ApiPort { get; set; }
        [JsonProperty("SshUser")]
        public string SshUser { get; set; }
        [JsonProperty("SshPassword")]
        public string SshPassword { get; set; }
        [JsonProperty("Gateway1")]
        public string Gateway1 { get; set; }
        [JsonProperty("CassCluster1")]
        public string CassCluster1 { get; set; }
        [JsonProperty("CassCluster2")]
        public string CassCluster2 { get; set; }
        [JsonProperty("CassCluster3")]
        public string CassCluster3 { get; set; }
    }

    public class JMeterConfig
    {
        [JsonProperty("SlaveIP")]
        public string SlaveIP { get; set; }
        [JsonProperty("JMeterExecutablePath")]
        public string JMeterExecutablePath { get; set; }
    }

    public class ExecutionEnvironment
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("OS")]
        public string OS { get; set; }
        [JsonProperty("Host")]
        public string Host { get; set; }
        [JsonProperty("LogFilePath")]
        public string LogFilePath { get; set; }
        [JsonProperty("DataFilePath")]
        public string DataFilePath { get; set; }
        [JsonProperty("TempPath")]
        public string TempPath { get; set; }
        [JsonProperty("JMeterConfig")]
        public JMeterConfig JMeterConfigObj = new JMeterConfig();
    }

    public class SauceLabVMSetting
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("SauceBrowser")]
        public string SauceBrowser { get; set; }
        [JsonProperty("SauceBrowserVersion")]
        public string SauceBrowserVersion { get; set; }
        [JsonProperty("SauceOS")]
        public string SauceOS { get; set; }
        [JsonProperty("CommandTimeout(secs)")]
        public string CommandTimeoutSecs { get; set; }
        [JsonProperty("Screen Resolution")]
        public string ScreenResolution { get; set; }
    }

    public class SeleniumTestingConfigurations
    {
        [JsonProperty("ElementLoadTime")]
        public string ElementLoadTime { get; set; }
    }

    public class SauceLabConfigurations
    {
        [JsonProperty("UseTunnel")]
        public string UseTunnel { get; set; }
        [JsonProperty("TunnelId")]
        public string TunnelId { get; set; }
        [JsonProperty("ParentTunnel")]
        public string ParentTunnel { get; set; }
        [JsonProperty("ParentKey")]
        public string ParentKey { get; set; }
        [JsonProperty("ExtendedDebugging")]
        public string ExtendedDebugging { get; set; }
        [JsonProperty("SeleniumRelayHost")]
        public string SeleniumRelayHost { get; set; }
        [JsonProperty("SeleniumRelayPort")]
        public string SeleniumRelayPort { get; set; }
        [JsonProperty("BuildTag")]
        public string BuildTag { get; set; }
        [JsonProperty("VMSettings")]
        public List<SauceLabVMSetting> SauceLabVMSettingsObj = new List<SauceLabVMSetting>();
    }

    public class NewRelicConfigurations
    {
        [JsonProperty("NewRelicKey")]
        public string NewRelicKey { get; set; }
    }

    public class CommonVariables
    {
        [JsonProperty("SystemConfigurations")]
        public List<SystemConfiguration> SystemConfigurationsObj = new List<SystemConfiguration>();
        [JsonProperty("ExecutionEnvironments")]
        public List<ExecutionEnvironment> ExecutionEnvironmentsObj = new List<ExecutionEnvironment>();
        [JsonProperty("SeleniumTestingConfigurations")]
        public SeleniumTestingConfigurations SeleniumTestingConfigurationsObj = new SeleniumTestingConfigurations();
        [JsonProperty("SauceLabConfigurations")]
        public SauceLabConfigurations SauceLabConfigurationsObj = new SauceLabConfigurations();
        [JsonProperty("NewRelicConfigurations")]
        public NewRelicConfigurations NewRelicConfigurationsObj = new NewRelicConfigurations();
    }
    #endregion json_class_common_config   
}

