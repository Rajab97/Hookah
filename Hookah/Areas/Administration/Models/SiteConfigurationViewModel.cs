using Hookah.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class SiteConfigurationViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.CallButtonText))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [MaxLength(50)]
        public string CallButtonText { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.InstagramLink))]
        [MaxLength(250)]
        public string InstagramLink { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.YoutubeLink))]
        [MaxLength(250)]
        public string YoutubeLink { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.TwitterLink))]
        [MaxLength(250)]
        public string TwitterLink { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.FacebookLink))]
        [MaxLength(250)]
        public string FacebookLink { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.PhoneNumberForCall))]
        [MaxLength(25)]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string PhoneNumberForCall { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.RequestCallButton))]
        [MaxLength(50)]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string RequestCallButton { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.InstagramProfileName))]
        [MaxLength(50)]
        public string InstagramProfileName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.CompanyName))]
        [MaxLength(50)]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ImageLP))]
        public string CompanyLogoImage { get; set; }

        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
        public List<string> FieldName { get; set; }
    }
}
