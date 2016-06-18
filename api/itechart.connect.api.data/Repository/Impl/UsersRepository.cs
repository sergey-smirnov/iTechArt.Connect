using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository.Impl
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(PerformanceReviewDbContext context)
            : base(context)
        {
        }

        public override IQueryable<User> Query(Expression<Func<User, bool>> predicate = null)
        {
            return base.Query(predicate)
                .Include(x => x.UserProfile)
                .Include(x => x.Department)
                .OrderBy("Department.Name")
                .ThenBy("UserProfile.FirstNameEng")
                .ThenBy("UserProfile.LastNameEng");
        }

        public IQueryable<User> QueryInDepartment(Guid departmentId, Expression<Func<User, bool>> predicate = null)
        {
            return base.Query(predicate)
                .Include(x => x.UserProfile)
                .Include(x => x.Department)
                .Where(x => x.DepartmentId == departmentId)
                .OrderBy("Department.Name")
                .ThenBy("UserProfile.FirstNameEng")
                .ThenBy("UserProfile.LastNameEng");
        }
    }
}