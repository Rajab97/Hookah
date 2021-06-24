using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface ICatheringServiceFacade
    {
        public Task<Result> SaveAsync(CatheringViewModel model);

        public Task<Result<CatheringViewModel>> GetDefaultModelAsync();
    }
}
