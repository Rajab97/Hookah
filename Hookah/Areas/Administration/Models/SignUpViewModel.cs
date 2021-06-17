using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class SignUpViewModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Email))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Email { get; set; }


        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FirstName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.LastName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Password))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ConfirmPassword))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string ConfirmPassword { get; set; }
    }
}
