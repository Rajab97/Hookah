using Hookah.Abstracts;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IContactService:IBaseService<Contact>
    {
        public Task<Result<Contact>> GetDefaultDataAsync();
    }
}
