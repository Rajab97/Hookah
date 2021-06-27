using Hookah.Abstracts;
using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class PackageItemViewModel:BaseViewModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.PackageItem))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(250)]
        public string Text { get; set; }
    }
}
