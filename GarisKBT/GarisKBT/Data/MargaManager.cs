using GarisKBT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GarisKBT.Data
{
    public class MargaManager
    {
        IRestService restService;
        public MargaManager(IRestService service) { restService = service; }
        public Task<List<Marga>> GetTasksAsync() { return restService.RefreshDataAsync(); }
        public Task SaveSearchDataAsync(string jsonData) { return restService.SaveSearchDataAsync(jsonData); }
    }
}
