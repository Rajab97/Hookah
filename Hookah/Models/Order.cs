using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class Order:BaseEntity
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? EventDate { get; set; }
        public int HourseOfService { get; set; }
        public string Address { get; set; }
        public string Additions { get; set; }

        public Guid? PackageId { get; set; }
        public Package Package { get; set; }
        public bool IsContacted { get; set; }
    }
}
