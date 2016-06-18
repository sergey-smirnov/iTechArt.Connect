using System.Collections.Generic;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Model;

namespace itechart.connect.api.Services.Synchronizers
{
    public interface IDataSynchonizer<T> where T : class, IEntityBase
    {
        Task<List<T>> SyncData(string sessionId = null, bool returnUpdatedItems = false);
    }
}