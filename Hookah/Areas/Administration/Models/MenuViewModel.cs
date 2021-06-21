using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.TopImageTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string ImageTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FlavorsTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FlavorsTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FlavorsText))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FlavorsText { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FruitHeadTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FruitHeadTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FruitHeadText))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FruitHeadText { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageLP))]
        public string ImageLP { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImagePL))]
        public string ImagePL { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageMB))]
        public string ImageMB { get; set; }

        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
