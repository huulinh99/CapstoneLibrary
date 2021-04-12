using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DetectionQueryFilter
    {
        public int? StaffId { get; set; }
        public string Url { get; set; }
        public string ImageThumbnail { get; set; }
        public string BookShelfName { get; set; }
        public int PageSize { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? Time { get; set; }

        public int PageNumber { get; set; }
    }
}
