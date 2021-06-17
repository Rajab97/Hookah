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
    public class QuickUser : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public QuickUser(IHttpContextAccessor httpContext, UserManager<User> userManager,
                            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _roleManager = roleManager;
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
