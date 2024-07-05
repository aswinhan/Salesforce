using SalesforceAPI.Data;
using SalesforceAPI.Models;
using SalesforceAPI.Repository.IRepostiory;

namespace SalesforceAPI.Repository
{
    public class PractitionerRepository : Repository<PractitionerFull>, IPractitionerRepository
    {
        private readonly ApplicationDbContext _db;
        public PractitionerRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
    }
}
