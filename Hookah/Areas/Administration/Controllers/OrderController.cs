using Hookah.Abstracts;
using Hookah.Constants;
using Hookah.Controllers;
using Hookah.Interfacas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hookah.Areas.Administration.Controllers
{
    [Authorize]
    [Area(AreaConstants.Admin)]
    public class OrderController : BaseController
    {
        public const string Name = "Order";
        private readonly IOrderServiceFacade _serviceFacade;

        public OrderController(IOrderServiceFacade serviceFacade)
        {
            this._serviceFacade = serviceFacade;
        }
        public IActionResult Index()
        {
            return View();
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
        public IActionResult GetData(GridFilterModel options)
        {
            var result = _serviceFacade.GetViewData();
            if (!result.IsSucceed)
            {
                return AjaxFailureResult(result);
            }
            return Filter(result.Data, options);
        }

        public async Task<IActionResult> Contact(Guid id)
        {
            var result = await _serviceFacade.ContactAsync(id);
            if (result.IsSucceed)
            {
                return Json("Ok");
            }
            return AjaxFailureResult(result);
        }
    }
}
