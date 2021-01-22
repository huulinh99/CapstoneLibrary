using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class NotificationQueryFilter
    {
        public int? Message { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
