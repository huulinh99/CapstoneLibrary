using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class CustomerQueryFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsNewest { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
