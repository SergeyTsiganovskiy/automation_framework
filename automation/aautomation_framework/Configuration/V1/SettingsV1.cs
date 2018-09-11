using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace aautomation_framework.Configuration.V1
{
    public static class SettingsV1
    {
        private static string ConfigurationFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"configurationV1.json");
        private static string TestStack => Environment.GetEnvironmentVariable("TestStack") ?? "afes-dev0";
        public static string Url { get; }

        public static DbConfigurationV1 Database { get; }

        public static UserV1 Manager { get; }

        public static UserV1 Analyst { get; }

        static SettingsV1()
        {
            var json = File.ReadAllText(ConfigurationFile);
            var configuration = JsonConvert.DeserializeObject<List<EnvironmentConfigurationV1>>(json)
                .FirstOrDefault(i => i.Name == TestStack);

            if (configuration == null)
            {
                throw new Exception($"There is no configuration in JSON file for test stack '{TestStack}'");
            }

            Url = configuration.Url;
            Database = configuration.Database;

            Manager = configuration.Users.FirstOrDefault(_ => _.Role.ToLowerInvariant() == "manager");
            Analyst = configuration.Users.FirstOrDefault(_ => _.Role.ToLowerInvariant() == "analyst");

        }
    }
}
