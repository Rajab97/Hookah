using Hookah.Areas.Administration.Models;
using Hookah.Interfacas;
using Hookah.Models;
using Hookah.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class FaqController : Controller
    {
        private readonly ISiteConfigurationServiceFacade _siteConfigurationServiceFacade;
        private readonly IFaqQuestionServiceFacade _faqQuestionServiceFacade;
        private readonly IFaqServiceFacade _faqServiceFacade;

        public FaqController(ISiteConfigurationServiceFacade siteConfigurationServiceFacade,
                                                    IFaqQuestionServiceFacade faqQuestionServiceFacade,
                                                     IFaqServiceFacade faqServiceFacade)
        {
            this._siteConfigurationServiceFacade = siteConfigurationServiceFacade;
            this._faqQuestionServiceFacade = faqQuestionServiceFacade;
            this._faqServiceFacade = faqServiceFacade;
        }
        public async Task<IActionResult> Index()
        {
            FaqViewModel faq = new FaqViewModel();
            SiteConfigurationViewModel siteConfiguration = new SiteConfigurationViewModel();
            IEnumerable<FaqQuestion> faqQuestions = new List<FaqQuestion>();


            var siteConfigurationResult = await _siteConfigurationServiceFacade.GetDefaultModelAsync();

            if (siteConfigurationResult.IsSucceed)
                siteConfiguration = siteConfigurationResult.Data;

            var questionResult = _faqQuestionServiceFacade.GetData();

            if (questionResult.IsSucceed)
                faqQuestions = questionResult.Data.ToList();

            var faqResult = await _faqServiceFacade.GetDefaultModelAsync();

            if (faqResult.IsSucceed)
                faq = faqResult.Data;

            FAQFullViewModel model = new FAQFullViewModel()
            {
                FaqQuestions = faqQuestions,
                FaqViewModel = faq,
                SiteConfigurationViewModel = siteConfiguration
            };
            return View(model);
        }
    }
}
