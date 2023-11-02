using GarisKBT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GarisKBT.Data
{
    public class MargaManager
    {
        IRestService restService;
        public MargaManager(IRestService service) { restService = service; }
        public Task<List<Marga>> GetTasksAsync() { return restService.RefreshDataAsync(); }
        public Task<List<Marga>> GetTasksAsyncSearchByName(string name) { return restService.GetTasksAsyncSearchByName(name); }
        public Task<string> GetGuidanceLink(string key) { return restService.GetGuidanceLink(key); }
        public Task<MargaDetail> GetMargaDetailAsync(int id) { return restService.GetMargaDetailAsync(id); }
        public Task<List<GetSilsilahResult>> GetSilsilahAsync(int id) { return restService.GetSilsilahAsync(id); }
        public Task<List<GetSilsilahResult>> GetChildsAsync(int id) { return restService.GetChildsAsync(id); }
        public Task SaveSearchDataAsync(string jsonData) { return restService.SaveSearchDataAsync(jsonData); }
    }
}
