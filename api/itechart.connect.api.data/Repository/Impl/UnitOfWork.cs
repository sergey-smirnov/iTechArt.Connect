using System;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository.Impl
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private Fields

        private readonly PerformanceReviewDbContext context;

        private IUsersRepository usersRepository;
        private IGenericRepository<EntitiesUpdateHistory> entitiesUpdateHistoryRepository;
        private IGenericRepository<UserProfile> userProfilessRepository;
        private IDepartmentsRepository departmentsRepository;
        private IGenericRepository<EntityType> entityTypesRepository;
        private IApplicationSettingsRepository applicationSettingsRepository;

        #endregion


        #region Properties

        public IUsersRepository UsersRepository => usersRepository ?? (usersRepository = new UsersRepository(context));

        public IGenericRepository<UserProfile> UserProfilessRepository => userProfilessRepository ?? (userProfilessRepository = new GenericRepository<UserProfile>(context));

        public IDepartmentsRepository DepartmentsRepository => departmentsRepository ?? (departmentsRepository = new DepartmentsRepository(context));

        public IGenericRepository<EntitiesUpdateHistory> EntitiesUpdateHistoryRepository => entitiesUpdateHistoryRepository ?? (entitiesUpdateHistoryRepository = new GenericRepository<EntitiesUpdateHistory>(context));

        public IGenericRepository<EntityType> EntityTypesRepository => entityTypesRepository ?? (entityTypesRepository = new GenericRepository<EntityType>(context));

        public IApplicationSettingsRepository ApplicationSettingsRepository => applicationSettingsRepository ?? (applicationSettingsRepository = new ApplicationSettingsRepository(context));

        #endregion

        public UnitOfWork(PerformanceReviewDbContext context)
        {
            this.context = context;
        }
        public UnitOfWork()
        {
            context = new PerformanceReviewDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable

        private bool disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}