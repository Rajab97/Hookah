using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class FooterFullViewModel
    {
        public IEnumerable<FooterGalaryItem> FooterGalaryItems { get; set; }

        public SiteConfigurationViewModel SiteConfiguration { get; set; }
    }
}
