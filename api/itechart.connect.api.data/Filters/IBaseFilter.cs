using System;
using System.Linq.Expressions;

namespace itechart.PerformanceReview.Data.Filters
{
    public interface IBaseFilter<TEntity>
    {
        Expression<Func<TEntity, bool>> ToPredicateExpression();
    }
}