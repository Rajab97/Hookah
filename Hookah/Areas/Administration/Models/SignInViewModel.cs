using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class SignInViewModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserPassword))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.RememberMe))]
        public bool RememberMe { get; set; }
    }
}
