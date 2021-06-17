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
    [Authorize()]
    public class PackageController : BaseController
    {
        public const string Name = "Package";
        private readonly IPackageServiceFacade _serviceFacade;

        public PackageController(IPackageServiceFacade serviceFacade)
        {
            this._serviceFacade = serviceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new PackageViewModel() {
                Items = new List<PackageItemViewModel>()
                {
                    new PackageItemViewModel()
                }
            };
            return View("Form", model);
        }

        public async Task<IActionResult> Save(PackageViewModel model)
        {
            var result = await _serviceFacade.SaveAsync(model);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }

        public IActionResult AddNewPackageItem()
        {
            var model = new PackageItemViewModel();
            return PartialView("_PackageItem",model);
        }
    }
}
