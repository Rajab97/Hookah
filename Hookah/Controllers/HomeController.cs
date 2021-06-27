using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.Servicefacades;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeServiceFacade _homeServiceFacade;
        private readonly IHomeLinkServiceFacade _homeLinkServiceFacade;
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;

        public HomeController(ILogger<HomeController> logger,
                                IHomeServiceFacade homeServiceFacade,
                                    IHomeLinkServiceFacade homeLinkServiceFacade,
                                        ISiteConfigurationServiceFacade siteConfigurationServiceFacade)
        {
            _logger = logger;
            _homeServiceFacade = homeServiceFacade;
            _homeLinkServiceFacade = homeLinkServiceFacade;
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
        }

        public async Task<IActionResult> Index()
        {
            HomeFullViewModel homeFullViewModel = new HomeFullViewModel() {
                HomeLinkViewModels = new List<HomeLinkViewModel>(),
                HomeViewModel = new HomeViewModel(),
                SiteConfiguration = new SiteConfigurationViewModel()
            };
            var homeModel = await _homeServiceFacade.GetDefaultModelAsync();
            var homeLinkModel =  _homeLinkServiceFacade.GetData();
            var siteConfigResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();
            if (homeModel.IsSucceed )
                homeFullViewModel.HomeViewModel = homeModel.Data;

            if (homeLinkModel.IsSucceed)
                homeFullViewModel.HomeLinkViewModels = homeLinkModel.Data;

            if (siteConfigResult.IsSucceed)
                homeFullViewModel.SiteConfiguration = siteConfigResult.Data;

            return View(homeFullViewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
