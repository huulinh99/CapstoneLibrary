using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DrawerDetectionQueryFilter
    {
        public int? DetectionId { get; set; }
        public int? DrawerId { get; set; }
        public DateTime? Time { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
