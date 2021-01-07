using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class CampaignQueryFilter
    {
        public int? StaffId { get; set; }

        public int PageSize { get; set; }
        public DateTime DateTime { get; set; }

        public int PageNumber { get; set; }
    }
}
