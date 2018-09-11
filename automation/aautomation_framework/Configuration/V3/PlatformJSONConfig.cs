using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace aautomation_framework.Configuration.V3
{
    public class JSONTestConfiguration
    {
        [JsonProperty("TestStack")]
        public string TestStack { get; set; }
        [JsonProperty("ExecutionHost")]
        public string ExecutionHost { get; set; }
        [JsonProperty("Clients")]
        public List<string> Clients = new List<string>();
        [JsonProperty("Internal_Users")]
        public List<string> Internal_Users = new List<string>();
        [JsonProperty("External_Users")]
        public List<string> External_Users = new List<string>();
    }

    public class JSONTestConfigurations
    {
        [JsonProperty("TestConfigurations")]
        public JSONTestConfiguration Configurations = new JSONTestConfiguration();
    }

    public class PlatformJSONConfig
    {
        public JSONTestConfigurations PLTConfigData = null;

        #region singleton
        private static PlatformJSONConfig instance;

        public static PlatformJSONConfig Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlatformJSONConfig();
                return instance;
            }
        }

        private PlatformJSONConfig()
        {
            using (StreamReader r = new StreamReader(TestContext.CurrentContext.TestDirectory + @"/../../../ExalinkPlatForm\DataSheets\PlatformTestConfiguration.json"))
            {
                string json = r.ReadToEnd();
                PLTConfigData = JsonConvert.DeserializeObject<JSONTestConfigurations>(json);
            }
        }
        #endregion singleton
    }
}

