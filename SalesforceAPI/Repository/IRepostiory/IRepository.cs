using System.Linq.Expressions;

namespace SalesforceAPI.Repository.IRepostiory
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProperties = null);
    }
}
