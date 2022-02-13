using GarisKBT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GarisKBT.Data
{
    public interface IRestService
    {
        Task<List<Marga>> RefreshDataAsync();
        Task<MargaDetail> GetMargaDetailAsync(int id);
        Task<List<GetSilsilahResult>> GetSilsilahAsync(int id);
        Task<List<GetSilsilahResult>> GetChildsAsync(int id);
        Task<string> GetGuidanceLink(string key);
        
        Task SaveSearchDataAsync(string jsonData);
    }
}
