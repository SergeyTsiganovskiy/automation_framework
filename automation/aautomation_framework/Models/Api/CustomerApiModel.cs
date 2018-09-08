using System.Collections.Generic;
using Newtonsoft.Json;

namespace aautomation_framework.Models.Api
{
    public class CustomerApiModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("plan_matching")]
        public List<string> PlanMatching { get; set; }
    }
}
