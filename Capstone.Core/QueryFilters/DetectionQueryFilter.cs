using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DetectionQueryFilter
    {
        public int StaffId { get; set; }
        public string Url { get; set; }
        public string ImageThumbnail { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
