using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class CallRequestGridViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EventDate { get; set; }
        public string AddedDate { get; set; }
        public string Message { get; set; }
        public string IsContacted { get; set; }
    }
}
