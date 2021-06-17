using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Abstracts
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Version { get; set; }
    }
}
