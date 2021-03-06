using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.ViewModels
{
    public class CallRequestViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FirstName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.LastName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.PhoneNumber))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [RegularExpression(pattern: @"^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$" , ErrorMessage = "The phone number is not in the correct format.(555) 555-1234")]
        [MaxLength(25)]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Email))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(100)]
        public string Email { get; set; }

        [Range(maximum:12,minimum:1 , ErrorMessageResourceType = typeof(ExceptionMessages) , ErrorMessageResourceName = nameof(ExceptionMessages.MonthField))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.MonthRequired))]
        public int? Month { get; set; }

        [Range(maximum: 31, minimum: 1, ErrorMessageResourceType = typeof(ExceptionMessages), ErrorMessageResourceName = nameof(ExceptionMessages.DayField))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.DayRequired))]
        public int? Day { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.YearRequired))]
        [Range(maximum: 2099, minimum: 2021, ErrorMessageResourceType = typeof(ExceptionMessages), ErrorMessageResourceName = nameof(ExceptionMessages.YearField))]
        public int? Year { get; set; }



        [Display(ResourceType = typeof(UI), Name = nameof(UI.Message))]
        [MaxLength(1000)]
        public string Message { get; set; }

    }
}
