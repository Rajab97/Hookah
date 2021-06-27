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
    public class FaqQuestionController : BaseController
    {
        public const string Name = "FaqQuestion";
        private readonly IFaqQuestionServiceFacade _FaqQuestionServiceFacade;

        public FaqQuestionController(IFaqQuestionServiceFacade FaqQuestionServiceFacade)
        {
            _FaqQuestionServiceFacade = FaqQuestionServiceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new FaqQuestionViewModel();
            return View("Form", model);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _FaqQuestionServiceFacade.RemoveAsync(id);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _FaqQuestionServiceFacade.GetModelAsync(id);
            if (result.IsSucceed)
            {
                return View("Form", result.Data);
            }
            return AjaxFailureResult(result);
        }
        public async Task<IActionResult> Save(FaqQuestionViewModel model)
        {
            var result = await _FaqQuestionServiceFacade.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }

        public IActionResult GetData(GridFilterModel options)
        {
            var result = _FaqQuestionServiceFacade.GetData();
            if (!result.IsSucceed)
            {
                return AjaxFailureResult(result);
            }
            return Filter(result.Data, options);
        }

    }
}
