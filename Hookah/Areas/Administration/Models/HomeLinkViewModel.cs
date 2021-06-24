using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class HomeLinkViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Title))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Name { get; set; }
        [Display(ResourceType = typeof(UI), Name = nameof(UI.PreviousUploadedImage))]
        public string ImagePath { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Link))]
        public string Link { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ButtonText))]
        public string ButtonText { get; set; }
        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
