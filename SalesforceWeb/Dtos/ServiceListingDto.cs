using Microsoft.AspNetCore.Mvc.Rendering;
using SalesforceWeb.Models;

namespace SalesforceWeb.Dtos
{
    public class ServiceListingDto
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<ServiceListing> records { get; set; }
    }
}
