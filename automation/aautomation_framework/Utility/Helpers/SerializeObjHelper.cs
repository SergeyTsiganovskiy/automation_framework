using Newtonsoft.Json;

namespace aautomation_framework.Utility.Helpers
{
    public class SerializeObjHelper
    {
        public static string SerializeHandlingNullValues<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
