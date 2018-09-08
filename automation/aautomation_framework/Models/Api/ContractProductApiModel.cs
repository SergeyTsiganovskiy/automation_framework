using System.Collections.Generic;

namespace aautomation_framework.Models.Api
{
    public class ContractProductApiModel : ApiResponseBaseModel<List<ContractProductApiModel>>
    {
        public string ndc11 { get; set; }
        public List<string> dates { get; set; }
    }
}
