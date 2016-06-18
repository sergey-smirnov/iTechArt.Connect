using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        PerformanceReviewDbContext Context { get; }
        TEntity GetById(object id);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> Query<TField>(Expression<Func<TEntity, TField>> orderBy, Expression<Func<TEntity, bool>> predicate = null, bool descending = false);
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TEntity>> FindAllAsync<TSortFields>(PagedFilter<TEntity, TSortFields> filter);
        TEntity Find(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntityDto Find<TEntityDto>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntityDto>> mapper) where TEntityDto : class;
        void Insert(TEntity entity, bool submitChanges = true);
        void Delete(object id, bool submitChanges = true);
        void Delete(TEntity entityToDelete, bool submitChanges = true);
        void Update(TEntity entityToUpdate, bool submitChanges = true);
        void SaveChanges();
    }
}