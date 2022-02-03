using GarisKBT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GarisKBT.Services
{
    public interface IMargaService
    {
        Task<IEnumerable<Marga>> GetMarga();
    }
}
