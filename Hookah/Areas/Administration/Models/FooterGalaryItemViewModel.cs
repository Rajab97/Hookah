using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class FooterGalaryItemViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Info))]
        public string Text { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.PreviousUploadedImage))]
        public string ImagePath { get; set; }

        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
