using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Authorization.Requirements
{
    public class OnlyActiveUsersRequirement:IAuthorizationRequirement
    {
    }
}
