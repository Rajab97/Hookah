using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class FAQFullViewModel
    {
        public FaqViewModel FaqViewModel { get; set; }
        public SiteConfigurationViewModel SiteConfigurationViewModel { get; set; }
        public IEnumerable<FaqQuestion> FaqQuestions { get; set; }
    }
}
