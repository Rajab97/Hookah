using Hookah.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class CallRequest : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? EventDate { get; set; }
        public bool IsContacted { get; set; }
    }
}
