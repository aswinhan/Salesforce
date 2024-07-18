namespace SalesforceWeb.Utilities
{
    public static class StaticData
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public static string SessionToken = "JWTToken";
    }
}
