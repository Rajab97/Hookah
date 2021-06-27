using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class ContactController : BaseController
    {
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;
        private readonly IContactServiceFacade _contactServiceFacade;
        private readonly ICallRequestServiceFacade _callRequestServiceFacade;

        public ContactController(ISiteConfigurationServiceFacade siteConfigurationServiceFacade,
                                                    IContactServiceFacade contactServiceFacade,
                                                     ICallRequestServiceFacade callRequestServiceFacade)
        {
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
            this._contactServiceFacade = contactServiceFacade;
            this._callRequestServiceFacade = callRequestServiceFacade;
        }
        public async Task<IActionResult> Index()
        {
            ContactViewModel contact = new ContactViewModel();
            SiteConfigurationViewModel siteConfiguration = new SiteConfigurationViewModel();
           
            var siteConfigurationResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();

            if (siteConfigurationResult.IsSucceed)
                siteConfiguration = siteConfigurationResult.Data;

            var contactResult = await _contactServiceFacade.GetDefaultModelAsync();

            if (contactResult.IsSucceed)
                contact = contactResult.Data;

            ContactFullViewModel model = new ContactFullViewModel()
            {
                ContactInfo = contact,
                SiteConfiguration = siteConfiguration
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SendCallRequest(CallRequestViewModel model)
        {
            var result = await _callRequestServiceFacade.SendCallRequestAsync(model);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }
    }
}
