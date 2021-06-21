using Hookah.Areas.Administration.Models;
using Hookah.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.ViewComponents
{
    public class Header : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public Header(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            if (user == null)
            {
                return View(new MyProfileAsideViewModel());
            }
           
            var model = new MyProfileAsideViewModel()
            {
                Name = user.FirstName
            };
            return View(model);
        }
    }
}
