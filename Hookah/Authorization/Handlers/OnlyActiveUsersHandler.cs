using Hookah.Authorization.Requirements;
using Hookah.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Authorization.Handlers
{
    public class OnlyActiveUsersHandler : AuthorizationHandler<OnlyActiveUsersRequirement>
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public OnlyActiveUsersHandler(IHttpContextAccessor httpContext,UserManager<User> userManager)
        {
            this._httpContext = httpContext;
            this._userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyActiveUsersRequirement requirement)
        {
           
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            if (user == null)
            {
                return;
            }

            if (!user.IsBlocked)
                context.Succeed(requirement);

        }
    }
}
