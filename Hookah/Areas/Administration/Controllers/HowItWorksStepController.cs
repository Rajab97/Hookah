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
    public class HowItWorksStepController : BaseController
    {
        public const string Name = "HowItWorksStep";
        private readonly IHowItWorksStepServiceFacade _serviceFacade;

        public HowItWorksStepController(IHowItWorksStepServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new HowItWorksStepViewModel();
            return View("Form",model);
        }
        public async Task<IActionResult> Remove(Guid id)
        {
            var resilt = await _serviceFacade.RemoveAsync(id);
            if (resilt.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(resilt);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _serviceFacade.GetModelAsync(id);
            if (result.IsSucceed)
            {
                return View("Form", result.Data);
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Save(HowItWorksStepViewModel model)
        {
            var result = await _serviceFacade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }

        public IActionResult GetData(GridFilterModel options)
        {
            var result = _serviceFacade.GetData();
            if (!result.IsSucceed)
            {
                return AjaxFailureResult(result);
            }
            return Filter(result.Data, options);
        }
    }
}
