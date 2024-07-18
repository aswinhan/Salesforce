using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace SalesforceAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<CredentialingContact> CredentialingContacts { get; set; }
        public DbSet<DirectService> DirectServices { get; set; }        
        public DbSet<OrganizationalCP> OrganizationalCredentialingProfiles { get; set; }
        public DbSet<ServiceLocation> ServiceLocations { get; set; }
        public DbSet<ServiceLocationLicense> ServiceLocationLicenses { get; set; }
        public DbSet<OrganizationalPSV> OrganizationalPrimarySourceVerifications { get; set; }
        public DbSet<ProviderKey> ProviderKeys { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<HospitalAffiliation> HospitalAffiliations { get; set; }
        public DbSet<PGMedicalTraining> PostGraduateMedicalTrainings { get; set; }
        public DbSet<PractitionerCP> PractitionerCredentialingProfiles { get; set; }
        public DbSet<PractitionerLC> PractitionerLicensesCertifications { get; set; }
        public DbSet<PractitionerPSV> PractitionerPrimarySourceVerifications { get; set; }
    }
}
