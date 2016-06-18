using System;
using System.Linq.Expressions;

namespace itechart.PerformanceReview.Data.Filters
{
    public abstract class BaseFilter<TEntity> : IBaseFilter<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> ToPredicateExpression();
    }
}