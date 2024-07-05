using SalesforceAPI.Data;
using SalesforceAPI.Models;
using SalesforceAPI.Repository.IRepostiory;

namespace SalesforceAPI.Repository
{
    public class OrganizationRepository : Repository<OrganizationFull>, IOrganizationRepository
    {
        private readonly ApplicationDbContext _db;
        public OrganizationRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
    }
}
