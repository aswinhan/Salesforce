namespace SalesforceWeb.Models
{
    public class ServiceListing
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Attributes attributes { get; set; }
    }
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class ServiceListingQueryResult
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<ServiceListing> records { get; set; }
    }

   
}
