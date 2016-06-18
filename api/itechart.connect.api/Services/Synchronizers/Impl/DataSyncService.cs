using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using itechart.connect.api.Helpers;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Repository;
using itechart.PerformanceReview.Data.Repository.Impl;

namespace itechart.connect.api.Services.Synchronizers.Impl
{
    public static class DataSyncService
    {
        #region private fields

        private static DateTime lastUpdateDateUtc;
        private static IDataSynchonizer<Department> departmentsSynchronizer;
        private static IDataSynchonizer<User> usersSynchonizer;

        //TODO: resolve with ninject
        private static readonly IUnitOfWork UnitOfwork = new UnitOfWork();

        #endregion

        #region properties

        public static IDataSynchonizer<Department> DepartmentsSynchronizer
        {
            get { return departmentsSynchronizer ?? (departmentsSynchronizer = new DepartmentsSynchronizer(UnitOfwork)); }
        }

        public static IDataSynchonizer<User> UsersSynchonizer
        {
            get { return usersSynchonizer ?? (usersSynchonizer = new UsersSynchronizer(UnitOfwork)); }
        }

        public static bool IsSynchronizationInProgress { get; private set; }

        #endregion


        public static async Task SynchronizeDataWithSmg(string sessionId = null)
        {
            if (IsSynchronizationInProgress || (String.IsNullOrEmpty(sessionId) && !SessionHelper.IsAuthenticated()))
            {
                return;
            }

            IsSynchronizationInProgress = true;
            var nowUtc = DateTime.UtcNow;

            try
            {
                var interval = nowUtc.Subtract(lastUpdateDateUtc).TotalMilliseconds;
                if (interval >= ApplicationSettingsService.Instance.Settings.MinDataSynchronizationInterval)
                {
                    try
                    {
                        await DepartmentsSynchronizer.SyncData(sessionId);
                        await UsersSynchonizer.SyncData(sessionId);
                    }
                    catch (AuthenticationException)
                    {
                        //TODO: add logic
                    }
                }
            }
            catch (Exception)
            {
                //TODO: catch and log sync errors
                throw;
            }
            finally
            {
                lastUpdateDateUtc = nowUtc;
                IsSynchronizationInProgress = false;
            }
        }
    }
}