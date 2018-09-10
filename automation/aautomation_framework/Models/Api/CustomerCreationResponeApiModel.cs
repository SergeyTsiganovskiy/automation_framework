namespace aautomation_framework.Models.Api
{
    public class CustomerCreationResponeApiModel : ApiResponseBaseModel<CustomerInfo>
    {
    }

    public class CustomerInfo
    {
        public string customer_id { get; set; }
    }
}
