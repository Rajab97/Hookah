using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class CatheringFullViewModel
    {
        public CatheringViewModel CatheringView { get; set; }
        public SiteConfigurationViewModel SiteConfigurationView { get; set; }
        public IEnumerable<CatheringEvent> CatheringEvents { get; set; }
        public IEnumerable<Package> Packages{ get; set; }
        public IEnumerable<HowItWorksStep> HowItWorksSteps { get; set; }
    }
}
