using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Interfacas
{
    public interface ISiteConfigurationServiceFacade
    {
        public Task<Result> SaveAsync(SiteConfigurationViewModel model);

        public Task<Result<SiteConfigurationViewModel>> GetDefaultModelAsync();
    }
}
