using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace aautomation_framework.Configuration.V2
{
    public class MCCommonConfigurationsVariablesModel
    {
        public MCActiveConfigurationVariables McActiveConfiguration { get; set; } =
            new MCActiveConfigurationVariables();

        public List<SystemConfiguration> CommonSystemConfigurations { get; set; } = new List<SystemConfiguration>();
    }

    public class MCActiveConfigurationVariables
    {
        public string Name { get; set; }

    }
}


