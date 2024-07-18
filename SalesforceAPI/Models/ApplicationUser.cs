using Microsoft.AspNetCore.Identity;

namespace SalesforceAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
