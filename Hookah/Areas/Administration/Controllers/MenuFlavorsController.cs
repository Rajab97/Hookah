using Hookah.Interfacas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Controllers
{
    public class MenuFlavorsController : Controller
    {
        public const string Name = "MenuFlavor";
        private readonly IMenuFlavorServiceFacade _menuFlavorServiceFacade;

        public MenuFlavorsController(IMenuFlavorServiceFacade menuFlavorServiceFacade)
        {
            _menuFlavorServiceFacade = menuFlavorServiceFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
