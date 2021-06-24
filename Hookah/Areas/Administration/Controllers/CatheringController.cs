﻿using Hookah.Areas.Administration.Models;
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
    public class CatheringController : BaseController
    {
        public const string Name = "Cathering";
        private readonly ICatheringServiceFacade _serviceFacade;

        public CatheringController(ICatheringServiceFacade serviceFacade)
        {
            this._serviceFacade = serviceFacade;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _serviceFacade.GetDefaultModelAsync();
            if (model.IsSucceed)
            {
                return View("Form", model.Data);
            }
            return View("Form", new CatheringViewModel());
        }

        public async Task<IActionResult> Save(CatheringViewModel model)
        {
            var result = await _serviceFacade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
    }
}
