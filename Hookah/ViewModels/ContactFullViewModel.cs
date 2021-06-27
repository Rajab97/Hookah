using Hookah.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class ContactFullViewModel
    {
        public SiteConfigurationViewModel SiteConfiguration { get; set; }
        public ContactViewModel ContactInfo { get; set; }
    }
}
