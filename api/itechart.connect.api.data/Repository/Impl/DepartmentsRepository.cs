using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository.Impl
{
    public class DepartmentsRepository : GenericRepository<Department>, IDepartmentsRepository
    {

        public DepartmentsRepository(PerformanceReviewDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Department> Query(Expression<Func<Department, bool>> predicate = null)
        {
            return base.Query(predicate)
                .Include(x => x.Users)
                .OrderByDescending(x => x.Name);
        }
    }
}