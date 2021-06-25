using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class SiteConfiguration : BaseEntity
    {
        public string InstagramLink { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitterLink { get; set; }
        public string FacebookLink { get; set; }
        public string PhoneNumberForCall { get; set; }
        public string CallButtonText { get; set; }
        public string RequestCallButton { get; set; }

        public string InstagramProfileName { get; set; }

        public string CompanyName { get; set; }
        public string CompanyLogoImage { get; set; }
    }
}
