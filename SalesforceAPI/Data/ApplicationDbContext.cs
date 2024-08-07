using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using SalesforceAPI.Dtos;

namespace SalesforceAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> AuditEntityChangesAsync(string userId = null)
        {
            bool hasChanges = false;
            var auditEntries = new List<AuditEntry>();

            ChangeTracker.DetectChanges();

            using (var transaction = await Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entry in ChangeTracker.Entries())
                    {
                        if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                            continue;

                        hasChanges = true;
                        var auditEntry = new AuditEntry(entry)
                        {
                            TableName = entry.Entity.GetType().Name,
                            UserId = userId
                        };
                        auditEntries.Add(auditEntry);

                        foreach (var property in entry.Properties)
                        {
                            string propertyName = property.Metadata.Name;
                            if (property.Metadata.IsPrimaryKey())
                            {
                                auditEntry.KeyValues[propertyName] = property.CurrentValue;
                                continue;
                            }

                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    auditEntry.AuditType = Enums.AuditType.Create;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    break;
                                case EntityState.Deleted:
                                    auditEntry.AuditType = Enums.AuditType.Delete;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    break;
                                case EntityState.Modified:
                                    if (property.IsModified)
                                    {
                                        auditEntry.ChangedColumns.Add(propertyName);
                                        auditEntry.AuditType = Enums.AuditType.Update;
                                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    }
                                    break;
                            }
                        }

                        // Detach the entry to ensure no changes are committed to other entities
                        entry.State = EntityState.Detached;
                    }

                    // Rollback the transaction to ensure no changes are committed to other entities
                    await transaction.RollbackAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            if (hasChanges)
            {
                try
                {
                    // Use a separate transaction to save audit logs
                    using (var auditTransaction = await Database.BeginTransactionAsync())
                    {
                        // Add and save the audit logs
                        AuditLogs.AddRange(auditEntries.Select(ae => ae.ToAudit()));
                        await SaveChangesAsync();
                        await auditTransaction.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving audit logs: {ex.Message}");
                    throw;
                }
            }

            return hasChanges;
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
