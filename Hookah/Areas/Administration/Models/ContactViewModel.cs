using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class ContactViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.TopImageTitle))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(100)]
        public string ImageTitle { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageLP))]
        public string ImageLP { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImagePL))]
        public string ImagePL { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageMB))]
        public string ImageMB { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FormTile))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(400)]
        public string FormTitle { get; set; }
        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
