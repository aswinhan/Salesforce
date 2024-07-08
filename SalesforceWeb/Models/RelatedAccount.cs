using System.Net;

namespace SalesforceWeb.Models
{
    public class RelatedAccount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address ShippingAddress { get; set; }
        public bool isAgency__c { get; set; }
        public bool isSite__c { get; set; }
        public bool isCMHSP__c { get; set; }
    }
    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string geocodeAccuracy { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string street { get; set; }
    }
}
