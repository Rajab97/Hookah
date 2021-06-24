using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface IHomeServiceFacade
    {
        public Task<Result> SaveAsync(HomeViewModel model);

        public Task<Result<HomeViewModel>> GetDefaultModelAsync();
    }
}
