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
        Task SaveSearchDataAsync(string jsonData);
    }
}
