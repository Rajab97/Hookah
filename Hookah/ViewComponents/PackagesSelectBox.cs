using Hookah.Interfacas;
using Hookah.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewComponents
{
    public class PackagesSelectBox : ViewComponent
    {
        private readonly IPackageService _packageService;

        public PackagesSelectBox(IPackageService packageService)
        {
            this._packageService = packageService;
        }
        public IViewComponentResult Invoke()
        {
            var packagesResult = _packageService.GetAll();
            if (packagesResult.IsSucceed)
            {
                return View(packagesResult.Data.ToList());
            }
            return View(new List<Package>());
        }
    }
}
