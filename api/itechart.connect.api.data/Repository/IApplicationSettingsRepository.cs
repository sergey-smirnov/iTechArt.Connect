using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Model;

namespace itechart.PerformanceReview.Data.Repository
{
    public interface IApplicationSettingsRepository : IGenericRepository<ApplicationSetting>
    {
        ApplicationSetting GetApplicationSettings();
    }
}