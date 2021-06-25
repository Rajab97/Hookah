using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class Faq:BaseEntity
    {
        public string ImageTitle { get; set; }
        public string ImageLP { get; set; }
        public string ImagePL { get; set; }
        public string ImageMB { get; set; }
    }
}
