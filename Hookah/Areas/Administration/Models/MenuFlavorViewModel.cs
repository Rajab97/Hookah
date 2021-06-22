using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class MenuFlavorViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FlavorsText))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Text { get; set; }
    }
}
