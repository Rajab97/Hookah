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
    public class CatheringEventController : BaseController
    {
        public const string Name = "CatheringEvent";
        private readonly ICatheringEventServiceFacade _CatheringEventerviceFacade;

        public CatheringEventController(ICatheringEventServiceFacade CatheringEventerviceFacade)
        {
            _CatheringEventerviceFacade = CatheringEventerviceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new CatheringEventViewModel();
            return View("Form", model);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _CatheringEventerviceFacade.RemoveAsync(id);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _CatheringEventerviceFacade.GetModelAsync(id);
            if (result.IsSucceed)
            {
                return View("Form", result.Data);
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Save(CatheringEventViewModel model)
        {
            var result = await _CatheringEventerviceFacade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }

        public IActionResult GetData(GridFilterModel options)
        {
            var result = _CatheringEventerviceFacade.GetData();
            if (!result.IsSucceed)
            {
                return AjaxFailureResult(result);
            }
            return Filter(result.Data, options);
        }

    }
}
