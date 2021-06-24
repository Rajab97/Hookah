using Hookah.Abstracts;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IHomeService : IBaseService<Home>
    {
        public Task<Result<Home>> GetDefaultDataAsync();
    }
}
