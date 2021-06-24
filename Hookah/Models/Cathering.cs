using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class Cathering : BaseEntity
    {
        public string ImageTitle { get; set; }
        public string ImageLP { get; set; }
        public string ImagePL { get; set; }
        public string ImageMB { get; set; }

        public string BaseTitle { get; set; }
        public string BaseTitleText { get; set; }
        public string BaseTitleBoldText { get; set; }

        public string SelectPackageTitle { get; set; }
        public string HowItWorksTitle { get; set; }
        public string OrderTitle { get; set; }
    }
}
