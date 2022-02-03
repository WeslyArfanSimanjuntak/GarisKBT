using GarisKBT.Models;
using GarisKBT.Services;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;



[assembly: Dependency(typeof(MargaService))]
namespace GarisKBT.Services
{
    public class MargaService : IMargaService
    {
        public async Task<IEnumerable<Marga>> GetMarga()
        {

            var marga = await App.MargaManager.GetTasksAsync();
            return marga;
        }
    }
}
