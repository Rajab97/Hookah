using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Abstracts
{
    public class GridFilterModel
    {
        public int Length { get; set; }
        public int Start { get; set; }

        public SearchModel Search { get; set; }

        public List<ColumnModel> Columns { get; set; }
        public List<OrderModel> Order { get; set; }
        public List<string> ColumnsDef { get; set; }

        public class OrderModel
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }

        public class SearchModel
        {
            public string Value { get; set; }
            public string Regex { get; set; }
        }
        public class ColumnModel
        {
            public string Data { get; set; }
            public string Name { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public SearchModel Search { get; set; }
        }
    }
}
