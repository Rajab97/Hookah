using Hookah.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize()]
    public class MenuController : Controller
    {
        public const string Name = "Menu";
        public MenuController()
        {
                
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
