using Hookah.Areas.Administration.Models;
using Hookah.Constants;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.ViewComponents
{
    public class MyProfileLeftAside : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public MyProfileLeftAside(IHttpContextAccessor httpContext, UserManager<User> userManager)
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
            var role = String.Empty;
            if (await _userManager.IsInRoleAsync(user, RoleNames.SuperAdmin))
                role = UI.SuperAdmin;
            else if (await _userManager.IsInRoleAsync(user, RoleNames.Admin))
                role = UI.Admin;
            var model = new MyProfileAsideViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Role = role
            };
            return View(model);
        }
    }
}
