using Hookah.Abstracts;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface ICatheringService : IBaseService<Cathering>
    {
        public Task<Result<Cathering>> GetDefaultDataAsync();
    }
}
