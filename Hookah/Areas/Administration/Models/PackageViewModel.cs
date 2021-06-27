using Hookah.Abstracts;
using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class PackageViewModel:BaseViewModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.Title))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ExtraHourPrice))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public decimal? ExtraHourPrice { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.InitialPrice))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public decimal? InitialPrice { get; set; }

        public ICollection<PackageItemViewModel> Items { get; set; }
    }
}
