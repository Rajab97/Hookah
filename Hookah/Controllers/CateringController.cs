using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class CateringController : BaseController
    {
        private readonly ICatheringServiceFacade _catheringServiceFacade;
        private readonly IPackageServiceFacade _packageServiceFacade;
        private readonly IHowItWorksStepServiceFacade _howItWorksStepServiceFacade;
        private readonly ICatheringEventServiceFacade _catheringEventServiceFacade;
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;
        private readonly IOrderServiceFacade _orderServiceFacade;

        public CateringController(ICatheringServiceFacade catheringServiceFacade,
                                    IPackageServiceFacade packageServiceFacade,
                                        IHowItWorksStepServiceFacade howItWorksStepServiceFacade,
                                            ICatheringEventServiceFacade catheringEventServiceFacade,
                                                ISiteConfigurationServiceFacade siteConfigurationServiceFacade,
                                                    IOrderServiceFacade orderServiceFacade)
        {
            this._catheringServiceFacade = catheringServiceFacade;
            this._packageServiceFacade = packageServiceFacade;
            this._howItWorksStepServiceFacade = howItWorksStepServiceFacade;
            this._catheringEventServiceFacade = catheringEventServiceFacade;
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
            this._orderServiceFacade = orderServiceFacade;
        }
        public async Task<IActionResult> Index()
        {
            CatheringViewModel cathering = null;
            SiteConfigurationViewModel siteConfiguration = null;
            IEnumerable<CatheringEvent> catheringEvents = new List<CatheringEvent>();
            IEnumerable<Package> packages = new List<Package>();
            IEnumerable<HowItWorksStep> howItWorksSteps= new List<HowItWorksStep>();

            var catheringConfigResult = await _catheringServiceFacade.GetDefaultModelAsync();

            if (!catheringConfigResult.IsSucceed)
                cathering = new CatheringViewModel();
            else
                cathering = catheringConfigResult.Data;

            var siteConfigurationResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();

            if (!siteConfigurationResult.IsSucceed)
                siteConfiguration = new SiteConfigurationViewModel();
            else
                siteConfiguration = siteConfigurationResult.Data;

            var catheringEventResult = _catheringEventServiceFacade.GetData();

            if (catheringEventResult.IsSucceed)
                catheringEvents = catheringEventResult.Data;

            var packagesResult = _packageServiceFacade.GetAll();

            if (packagesResult.IsSucceed)
                packages = packagesResult.Data.Include(m=>m.Items);

            var howItWorksStepsResult = _howItWorksStepServiceFacade.GetData();

            if (howItWorksStepsResult.IsSucceed)
                howItWorksSteps = howItWorksStepsResult.Data;


            CatheringFullViewModel model = new CatheringFullViewModel()
            {
                CatheringEvents = catheringEvents,
                CatheringView = cathering,
                HowItWorksSteps = howItWorksSteps,
                Packages = packages,
                SiteConfigurationView = siteConfiguration
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SendOrder(OrderViewModel model)
        {
            var result = await _orderServiceFacade.SendOrderAsync(model);
            if (result.IsSucceed)
                return Json("Ok");
            return AjaxFailureResult(result);
        }
    }
}
