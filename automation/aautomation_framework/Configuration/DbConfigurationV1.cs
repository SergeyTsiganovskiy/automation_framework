using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aautomation_framework.Configuration
{
    public class DbConfigurationV1
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
