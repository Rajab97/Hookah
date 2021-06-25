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
        

        public HomeController(ILogger<HomeController> logger,IHomeServiceFacade homeServiceFacade,IHomeLinkServiceFacade homeLinkServiceFacade)
        {
            _logger = logger;
            _homeServiceFacade = homeServiceFacade;
            _homeLinkServiceFacade = homeLinkServiceFacade;
        }

        public async Task<IActionResult> Index()
        {
            var homeModel = await _homeServiceFacade.GetDefaultModelAsync();
            var homeLinkModel =  _homeLinkServiceFacade.GetData();

            if (homeModel.IsSucceed && homeLinkModel.IsSucceed)
            {
                HomeFullViewModel homeFullViewModel = new HomeFullViewModel();
                homeFullViewModel.HomeViewModel = homeModel.Data;
                homeFullViewModel.HomeLinkViewModels = homeLinkModel.Data;
                return View(homeFullViewModel);
            }
            return View(new HomeFullViewModel());
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
