using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Filters;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository.Impl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Properties

        //        protected Expression<Func<TEntity, TKey>> promaryKeySelector 

        public PerformanceReviewDbContext Context { get; private set; }

        #endregion

        private readonly DbSet<TEntity> dbSet;

        private DbSet<TEntity> DbSet
        {
            get
            {
                return null;
            }
        }

        public GenericRepository(PerformanceReviewDbContext context)
        {
            Context = context;

            //            context.Database.CreateIfNotExists();

            dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return Query(predicate).ToList();
            }
            return dbSet.ToList();
        }

        public virtual async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await Query(predicate).ToListAsync();
            }
            return await dbSet.ToListAsync();
        }

        public Task<List<TEntity>> FindAllAsync<TSortFields>(PagedFilter<TEntity, TSortFields> filter)
        {
            return Query().FilterQuery(filter).ToListAsync();
        }

        public virtual IQueryable<TEntity> Query<TField>(Expression<Func<TEntity, TField>> orderBy, Expression<Func<TEntity, bool>> predicate = null, bool descending = false)
        {
            var query = Query(predicate);

            query = @descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            return query;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return dbSet.Where(predicate).AsQueryable();
            }
            return dbSet.AsQueryable();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return Query().FirstOrDefault();
            }
            return Query(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await Query().FirstOrDefaultAsync();
            }
            return await Query(predicate).FirstOrDefaultAsync();
        }

        public virtual TEntityDto Find<TEntityDto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TEntityDto>> mapper) where TEntityDto : class
        {
            var entity = dbSet.FirstOrDefault(predicate);
            if (entity != null)
            {
                var method = mapper.Compile();
                return method.Invoke(entity);
            }
            return null;
        }

        public virtual void Insert(TEntity entity, bool submitChanges = true)
        {
            dbSet.Add(entity);
            if (submitChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Delete(object id, bool submitChanges = true)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);

            if (submitChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entityToDelete, bool submitChanges = true)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

            if (submitChanges)
            {
                Context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entityToUpdate, bool submitChanges = true)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;

            if (submitChanges)
            {
                Context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}