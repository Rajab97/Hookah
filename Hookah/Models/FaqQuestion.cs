using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class FaqQuestion:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
