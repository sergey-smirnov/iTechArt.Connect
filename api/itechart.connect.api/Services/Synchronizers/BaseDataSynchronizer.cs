using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Repository;

namespace itechart.connect.api.Services.Synchronizers
{
    public abstract class BaseDataSynchronizer<T> : IDataSynchonizer<T> where T : class, IEntityBase
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseDataSynchronizer(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public abstract Task<List<T>> SyncData(string sessionId = null, bool returnUpdatedItems = false);

        #region protected methods

        protected async Task<DateTime> GetLastUpdatedDate(EntityType entityType)
        {
            var lastUpdatedDate =
                await
                    UnitOfWork.EntitiesUpdateHistoryRepository.Query(x => x.EntityTypeId == entityType.EntityTypeId)
                        .OrderByDescending(x => x.UpdatedDateUtc)
                        .Select(x => x.UpdatedDateUtc)
                        .FirstOrDefaultAsync();
            return lastUpdatedDate;
        }

        protected void SaveUpdateToHistory(EntityType entityType, DateTime nowUtc)
        {
            var entitiesUpdateHistory = new EntitiesUpdateHistory
            {
                EntityTypeId = entityType.EntityTypeId,
                UpdatedDateUtc = nowUtc
            };
            UnitOfWork.EntitiesUpdateHistoryRepository.Insert(entitiesUpdateHistory);
        }

        #endregion
    }
}