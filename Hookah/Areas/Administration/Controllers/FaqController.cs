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
    public class FaqController : BaseController
    {
        public const string Name = "Faq";
        private readonly IFaqServiceFacade _service;

        public FaqController(IFaqServiceFacade service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
           var model =  await _service.GetDefaultModelAsync();
            if (model.IsSucceed)
            {
                return PartialView("Form",model.Data);
            }
            return PartialView("Form", new FaqViewModel());
        }

        public async Task<IActionResult> Save(FaqViewModel model)
        {
            var result = await _service.SaveAsync(model);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
    }
}
