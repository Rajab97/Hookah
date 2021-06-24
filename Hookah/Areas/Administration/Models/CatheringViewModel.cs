using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class CatheringViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.TopImageTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string ImageTitle { get; set; }



        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageLP))]
        public string ImageLP { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImagePL))]
        public string ImagePL { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageMB))]
        public string ImageMB { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.BaseTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string BaseTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.BaseTitleText))]
        public string BaseTitleText { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.BaseTitleBoldText))]
        public string BaseTitleBoldText { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.SelectPackageTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string SelectPackageTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.HowItWorksTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string HowItWorksTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.OrderTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string OrderTitle { get; set; }


        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
