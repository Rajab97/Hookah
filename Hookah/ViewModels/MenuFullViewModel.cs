using Hookah.Areas.Administration.Models;
using Hookah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class MenuFullViewModel
    {
        public SiteConfigurationViewModel SiteConfiguration { get; set; }
        public MenuViewModel MenuViewModel { get; set; }
        public IEnumerable<MenuFlavor> MenuFlavorViews { get; set; }
        public IEnumerable<MenuFruitHead> MenuFruitHeadViews { get; set; }
    }
}
