using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Data;

namespace SalesforceAPI.Controllers.Services
{
    public class ProviderService
    {
        private readonly ApplicationDbContext _context;

        public ProviderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int?> GetProviderIdAsync(string credentialingProfileId)
        {
            return await _context.ProviderKeys
            .AsNoTracking()
            .Where(x => x.CredentialingProfileId == credentialingProfileId)
            .Select(x => x.ProviderId)
            .FirstOrDefaultAsync();
        }
    }
}
