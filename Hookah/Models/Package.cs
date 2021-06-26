using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class Package : BaseEntity
    {
        public string Title { get; set; }
        public decimal ExtraHourPrice { get; set; }
        public decimal InitialPrice { get; set; }
        public ICollection<PackageItem> Items { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
