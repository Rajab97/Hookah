using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class HomeLink : BaseEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImagePath { get; set; }
        public string ButtonText { get; set; }
    }
}
