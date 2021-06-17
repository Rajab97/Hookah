using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class PackageItem : BaseEntity
    {
        public string Text { get; set; }
        public Guid PackageId { get; set; }
        public Package Package { get; set; }
    }
}
