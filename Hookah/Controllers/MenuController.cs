using Hookah.Interfacas;
using Hookah.Models;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class MenuController : BaseController
    {
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;
        private readonly IMenuFlavorServiceFacade _menuFlavorServiceFacade;
        private readonly IMenuFruitHeadServiceFacade _menuFruitHeadServiceFacade;
        private readonly IMenuServiceFacade _menuServiceFacade;

        public MenuController(ISiteConfigurationServiceFacade siteConfigurationServiceFacade,
                                IMenuFlavorServiceFacade menuFlavorServiceFacade,
                                    IMenuFruitHeadServiceFacade menuFruitHeadServiceFacade,
                                      IMenuServiceFacade menuServiceFacade)
        {
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
            this._menuFlavorServiceFacade = menuFlavorServiceFacade;
            this._menuFruitHeadServiceFacade = menuFruitHeadServiceFacade;
            this._menuServiceFacade = menuServiceFacade;
        }
        public async Task<IActionResult> Index()
        {
            var model = new MenuFullViewModel()
            {
                MenuFlavorViews = new List<MenuFlavor>(),
                MenuFruitHeadViews = new List<MenuFruitHead>(),
                MenuViewModel = new Areas.Administration.Models.MenuViewModel(),
                SiteConfiguration = new Areas.Administration.Models.SiteConfigurationViewModel()
            };

            var flavorsResult = _menuFlavorServiceFacade.GetData();

            if (flavorsResult.IsSucceed)
                model.MenuFlavorViews = flavorsResult.Data;

            var fruitHeadsResult = _menuFruitHeadServiceFacade.GetData();

            if (fruitHeadsResult.IsSucceed)
                model.MenuFruitHeadViews = fruitHeadsResult.Data;

            var menuResult =await _menuServiceFacade.GetDefaultModelAsync();

            if (menuResult.IsSucceed)
                model.MenuViewModel = menuResult.Data;

            var siteConfigResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();

            if (siteConfigResult.IsSucceed)
                model.SiteConfiguration = siteConfigResult.Data;

            return View(model);
        }
    }
}
