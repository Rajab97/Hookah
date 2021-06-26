using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewComponents
{
    public class Footer : ViewComponent
    {
        private readonly IFooterGalaryItemServiceFacade _footerGalaryItemServiceFacade;
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;

        public Footer(IFooterGalaryItemServiceFacade footerGalaryItemServiceFacade,
                        ISiteConfigurationServiceFacade siteConfigurationServiceFacade)
        {
            this._footerGalaryItemServiceFacade = footerGalaryItemServiceFacade;
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new FooterFullViewModel() {
                SiteConfiguration = new SiteConfigurationViewModel(),
                FooterGalaryItems = new List<FooterGalaryItem>()
            };

            var gallaryItemsResult = _footerGalaryItemServiceFacade.GetData();
            if (gallaryItemsResult.IsSucceed)
            {
                viewModel.FooterGalaryItems = gallaryItemsResult.Data;
            }

            var siteConfigResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();
            if (siteConfigResult.IsSucceed)
            {
                viewModel.SiteConfiguration = siteConfigResult.Data;
            }
            return View(viewModel);
        }
    }
}
