using aautomation_framework.Models.Api;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace aautomation_framework.Utility.Helpers.API
{
    public class CustomerHelper
    {
        private readonly ApiCallsClient apiCallsClient;
        private readonly Dictionary<string, string> xClient;

        public CustomerHelper(ApiCallsClient apiCallsClient, Dictionary<string, string> xClient)
        {
            this.apiCallsClient = apiCallsClient;
            this.xClient = xClient;
        }

        public CustomerHelper(ApiCallsClient apiCallsClient)
        {
            this.apiCallsClient = apiCallsClient;
        }

        public CustomerCreationResponeApiModel CreateCustomer(CustomerApiModel customerApiModel)
        {
            return apiCallsClient.SendPostRequest<CustomerCreationResponeApiModel>(
                apiCallsClient.GetEndPoint(McApiPaths.CustomerCreationApiPath), xClient,
                JsonConvert.SerializeObject(customerApiModel));
        }

        /// <summary>
        /// Method for creating MC customer via API
        /// </summary>
        /// <param name="customerModel">Customer API model that will be used as POST body</param>
        /// <param name="headers"></param>
        /// <returns>Created customer ID</returns>
        public string CreateCustomer(CustomerApiModel customerModel, Dictionary<string, string> headers)
        {
            var body = JsonConvert.SerializeObject(customerModel);
            var response = apiCallsClient.SendPostRequest(apiCallsClient.GetEndPoint(McApiPaths.CustomerCreationApiPath), headers, body);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Could not create a client. Status: {response.StatusCode}");
            }

            return JsonConvert.DeserializeObject<dynamic>(response.Content).data.customer_id.ToString();
        }

        public IRestResponse CreateCustomerViaApi(CustomerApiModel customerModel, Dictionary<string, string> headers, out string id)
        {
            var response = apiCallsClient.SendPostRequest(apiCallsClient.GetEndPoint(McApiPaths.CustomerCreationApiPath), headers, SerializeObjHelper.SerializeHandlingNullValues(customerModel));
            id = JsonConvert.DeserializeObject<CustomerCreationResponeApiModel>(response.Content).data.customer_id;
            return response;
        }

        public Dictionary<string, string> CreateCustomersParallel(List<CustomerApiModel> customerModels, Dictionary<string, string> headers)
        {
            List<Task> tasks = new List<Task>();
            Dictionary<string, string> customers = new Dictionary<string, string>();
            foreach (var model in customerModels)
            {
                tasks.Add(Task.Run(() => customers.Add(CreateCustomer(model, headers), model.Name)));
            }

            Task.WaitAll(tasks.ToArray());
            return customers;
        }
    }
}
