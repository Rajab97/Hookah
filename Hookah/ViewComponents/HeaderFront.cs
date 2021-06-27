using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewComponents
{
    public class HeaderFront : ViewComponent
    {
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;

        public HeaderFront(ISiteConfigurationServiceFacade siteConfigurationServiceFacade)
        {
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteConfigResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();
            if (siteConfigResult.IsSucceed)
            {
                return View(siteConfigResult.Data);
            }
            return View(new SiteConfigurationViewModel());
           
        }
    }
}
