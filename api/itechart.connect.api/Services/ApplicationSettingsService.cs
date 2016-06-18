using System.Web;
using itechart.PerformanceReview.Data.Model;
using itechart.PerformanceReview.Data.Repository;
using itechart.PerformanceReview.Data.Repository.Impl;
using Microsoft.AspNet.Identity.Owin;

namespace itechart.connect.api.Services
{
    public class ApplicationSettingsService
    {
        #region private fields

        private readonly IUnitOfWork unitOfWork;
        private ApplicationSetting settings;

        private object lockObject = new object();

        #endregion

        #region properties

        public ApplicationSetting Settings
        {
            get
            {
                if (settings != null)
                {
                    return settings;
                }

                lock (lockObject)
                {
                    settings = unitOfWork.ApplicationSettingsRepository.GetApplicationSettings();
                }

                return settings;
            }
        }


        #region sigleton

        private static ApplicationSettingsService instance;

        public static ApplicationSettingsService Instance
        {
            get
            {
                if (instance == null)
                {
                    if (HttpContext.Current != null)
                    {
                        var dbContext = HttpContext.Current.GetOwinContext().Get<PerformanceReviewDbContext>();
                        instance = new ApplicationSettingsService(new UnitOfWork(dbContext));
                    }
                    else
                    {
                        instance = new ApplicationSettingsService(new UnitOfWork());
                    }
                }
                return instance;
            }
        }

        #endregion

        #endregion

        #region ctor

        private ApplicationSettingsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion
    }
}