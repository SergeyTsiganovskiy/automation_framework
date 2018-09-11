using System.Collections.Generic;

namespace aautomation_framework.Configuration.V1
{
    public class EnvironmentConfigurationV1
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DbConfigurationV1 Database { get; set; }
        public List<UserV1> Users { get; set; }
    }
}
