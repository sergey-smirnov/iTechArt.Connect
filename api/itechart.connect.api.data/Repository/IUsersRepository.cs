using System;
using System.Linq;
using System.Linq.Expressions;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        IQueryable<User> QueryInDepartment(Guid departmentId, Expression<Func<User, bool>> predicate = null);
    }
}