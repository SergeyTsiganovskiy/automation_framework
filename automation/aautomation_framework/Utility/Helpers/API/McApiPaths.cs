namespace aautomation_framework.Utility.Helpers.API
{
    public class McApiPaths
    {
        // RELATED TO CUSTOMER CREATION

        public const string CustomerCreationApiPath = "/mccustomers/v1/customers";
        public const string PaymentTypeAddingApiPath = "/mccustomers/v1/customers/{0}/payment_methods";
        public const string PlanAddingApiPath = "/mccustomers/v1/customers/{0}/plans";
        public const string RoleAssigningApiPath = "/mcuser/v1/roles";
    }
}
