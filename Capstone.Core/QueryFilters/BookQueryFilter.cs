using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookQueryFilter
    {
        public string BookGroupId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
