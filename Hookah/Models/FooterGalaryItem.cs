using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class FooterGalaryItem:BaseEntity
    {
        public string Text { get; set; }
        public string ImagePath { get; set; }
    }
}
