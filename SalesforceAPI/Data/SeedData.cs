using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Models;

namespace SalesforceAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Delete existing RolePermissions data
                context.RolePermissions.RemoveRange(context.RolePermissions);
                await context.SaveChangesAsync();

                // Seed Screens
                if (!context.Screens.Any())
                {
                    var screens = new List<Screen>
                    {
                        new Screen { Name = "Credentialing" },
                        new Screen { Name = "RelatedAccount" },
                        new Screen { Name = "ServiceListing" },
                        new Screen { Name = "Practitioner" },
                        new Screen { Name = "Organization" },
                        new Screen { Name = "Audit" }
                    };

                    context.Screens.AddRange(screens);
                    await context.SaveChangesAsync();
                }

                // Seed Permissions
                if (!context.Permissions.Any())
                {
                    var permissions = new List<Permission>
                    {
                        new Permission { Name = "View" },
                        new Permission { Name = "Edit" },
                        new Permission { Name = "Delete" },
                        new Permission { Name = "Update" }
                    };

                    context.Permissions.AddRange(permissions);
                    await context.SaveChangesAsync();
                }

                // Seed RolePermissions for Admin Role
                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "admin");

                if (adminRole != null)
                {
                    var screenMappings = new Dictionary<string, List<string>>
                    {
                        { "Credentialing", new List<string> { "View", "Edit" } },
                        { "RelatedAccount", new List<string> { "View", "Edit" } },
                        { "ServiceListing", new List<string> { "View", "Edit" } },
                        { "Practitioner", new List<string> { "View", "Edit", "Delete", "Update" } },
                        { "Organization", new List<string> { "View", "Edit", "Delete", "Update" } },
                        { "Audit", new List<string> { "View" } }
                    };

                    foreach (var screenMapping in screenMappings)
                    {
                        var screen = context.Screens.FirstOrDefault(s => s.Name == screenMapping.Key);
                        foreach (var permissionName in screenMapping.Value)
                        {
                            var permission = context.Permissions.FirstOrDefault(p => p.Name == permissionName);
                            if (screen != null && permission != null)
                            {
                                var rolePermission = new RolePermission
                                {
                                    RoleId = adminRole.Id,
                                    ScreenId = screen.Id,
                                    PermissionId = permission.Id
                                };
                                context.RolePermissions.Add(rolePermission);
                            }
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
