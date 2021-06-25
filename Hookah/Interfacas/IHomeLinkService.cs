using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IHomeLinkService : IBaseService<HomeLink>
    {
        public Task<Result<HomeLinkViewModel>> GetDefaultModelAsync();
    }
}
