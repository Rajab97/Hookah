using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class HowItWorksStepViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.StepText))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(500)]
        public string Text { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.OrderNumber))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public int OrderNumber { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.PreviousUploadedImage))]
        public string ImagePath { get; set; }

        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
