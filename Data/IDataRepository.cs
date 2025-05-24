using ExportApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExportApp.Data
{
    public interface IDataRepository
    {
        Task<List<DataModel>> GetDataAsync();
    }
}
