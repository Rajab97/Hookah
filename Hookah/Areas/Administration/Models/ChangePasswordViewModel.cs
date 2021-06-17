using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class ChangePasswordViewModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.CurrentPassword))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string CurrentPassword { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.NewPassword))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string NewPasword { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ConfirmPassowrd))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string ConfirmPassword { get; set; }
    }
}
