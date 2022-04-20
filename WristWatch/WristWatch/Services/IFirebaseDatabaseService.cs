using WristWatch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WristWatch.Services
{
    public interface IFirebaseDatabaseService
    {
        List<Watch> GetAllWatches();

        Watch GetWatchById(string id);

        Task AddWatch(Watch Data);

        Task UpdateWatch(string id, Watch Data);
    }
}
