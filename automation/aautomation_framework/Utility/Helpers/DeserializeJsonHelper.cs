using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace aautomation_framework.Utility.Helpers
{
    public class DeserializeJsonHelper
    {
        public static string JsonPath(string jsonLocation)
        {
            string baseFilePath = AppDomain.CurrentDomain.BaseDirectory.Split(new[] { "ExaLinkMC" }, StringSplitOptions.None)[0];
            var inputJson = File.ReadAllText(Path.Combine(baseFilePath, jsonLocation));
            return inputJson;
        }

        public static T DeserializeJson<T>(string jsonLocation)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(
                    File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        jsonLocation)));
            }
            catch (Exception ex)
            {
                LogUtil.WriteDebug(ex.GetType().Name + ": " + ex.Message + ": " + ex.StackTrace);
                throw;
            }
        }

        public static T TryParseJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonSerializationException)
            {
                LogUtil.WriteDebug($"{json} cannot be deserialized with {nameof(T)}");
                return default(T);
            }
        }
    }
}

