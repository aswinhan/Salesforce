using Newtonsoft.Json;

namespace SalesforceWeb.Utilities
{
    public static class JsonUtility
    {
        public static string Format(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return "";

            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch (JsonReaderException)
            {
                return "Invalid JSON format";
            }
        }
    }
}
