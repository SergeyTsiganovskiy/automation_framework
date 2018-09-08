using System;

namespace aautomation_framework.Models.Api
{
    public class ApiResponseBaseModel<T>
    {
        public DateTime response_datetime { get; set; }
        public string status { get; set; }
        public T data { get; set; }
    }
}
