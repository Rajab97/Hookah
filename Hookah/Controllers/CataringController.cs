using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Controllers
{
    public class CataringController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
