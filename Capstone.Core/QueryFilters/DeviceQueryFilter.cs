using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DeviceQueryFilter
    {
        public string DeviceToken { get; set; }
        public int? CustomerId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
