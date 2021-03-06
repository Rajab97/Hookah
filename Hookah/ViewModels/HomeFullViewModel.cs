using Hookah.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class HomeFullViewModel
    {
        public HomeViewModel HomeViewModel { get; set; }
        public SiteConfigurationViewModel SiteConfiguration { get; set; }
        public IEnumerable<HomeLinkViewModel> HomeLinkViewModels { get; set; }
    }
}
