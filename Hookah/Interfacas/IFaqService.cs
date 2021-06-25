using Hookah.Abstracts;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IFaqService:IBaseService<Faq>
    {
        public Task<Result<Faq>> GetDefaultDataAsync();
    }
}
