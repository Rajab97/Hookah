using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Constants;
using Hookah.Controllers;
using Hookah.Interfacas;
using Hookah.Resources;
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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? packageId)
        {
            if (!packageId.HasValue)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.FatalError));

            var result = await _serviceFacade.GetModelByIdAsync(packageId.Value);

            if (result.IsSucceed)
                return View("Form", result.Data);

            return AjaxFailureResult(result);
        }
        [HttpPost]
        public IActionResult GetPackages(GridFilterModel options)
        {
            var result = _serviceFacade.GetAll();
            if (result.IsSucceed)
                return Filter(result.Data,options);
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Save(PackageViewModel model)
        {
            var result = await _serviceFacade.SaveAsync(model);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _serviceFacade.RemoveAsync(id);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }
        public IActionResult AddNewPackageItem()
        {
            var model = new PackageItemViewModel();
            return PartialView("_PackageItem",model);
        }

        [HttpGet]
        public IActionResult GetPackageItems(Guid? packageId)
        {
            if (!packageId.HasValue)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.FatalError));

            var result = _serviceFacade.GetPackageItems(packageId.Value);
            if (!result.IsSucceed)
                return AjaxFailureResult(result);

            return PartialView("_PackageItemIndex",result.Data);
        }
    }
}
