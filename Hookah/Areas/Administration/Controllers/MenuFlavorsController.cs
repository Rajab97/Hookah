using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Constants;
using Hookah.Controllers;
using Hookah.Interfacas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize]
    public class MenuFlavorsController : BaseController
    {
        public const string Name = "MenuFlavors";
        private readonly IMenuFlavorServiceFacade _menuFlavorServiceFacade;

        public MenuFlavorsController(IMenuFlavorServiceFacade menuFlavorServiceFacade)
        {
            _menuFlavorServiceFacade = menuFlavorServiceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new MenuFlavorViewModel();
            return View("Form", model);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _menuFlavorServiceFacade.RemoveAsync(id);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _menuFlavorServiceFacade.GetModelAsync(id);
            if (result.IsSucceed)
            {
                return View("Form", result.Data);
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Save(MenuFlavorViewModel model)
        {
            var result = await _menuFlavorServiceFacade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }

        public IActionResult GetData(GridFilterModel options)
        {
            var result = _menuFlavorServiceFacade.GetData();
            if (!result.IsSucceed)
            {
                return AjaxFailureResult(result);
            }
            return Filter(result.Data, options);
        }

    }
}
