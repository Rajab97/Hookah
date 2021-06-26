using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Models
{
    public class OrderGridViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EventDate { get; set; }
        public string AddedDate { get; set; }
        public int HourseOfService { get; set; }
        public string Address { get; set; }
        public string Additions { get; set; }
        public string Title { get; set; }
        public string IsContacted { get; set; }
    }
}
