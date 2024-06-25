using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Models;
using System.Collections.Generic;

namespace SalesforceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CredentialingContact> CredentialingContacts { get; set; }
        public DbSet<DirectService> DirectServices { get; set; }        
        public DbSet<OrganizationalCredentialingProfile> OrganizationalCredentialingProfiles { get; set; }
        public DbSet<ServiceLocation> ServiceLocations { get; set; }
        public DbSet<ServiceLocationLicense> ServiceLocationLicenses { get; set; }
        public DbSet<OrganizationalPrimarySourceVerification> OrganizationalPrimarySourceVerifications { get; set; }
        public DbSet<ProviderKey> ProviderKeys { get; set; }
    }
}
