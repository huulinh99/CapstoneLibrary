using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookDetectQueryFilter
    {
        public int? BookId { get; set; }
        public int? StaffId { get; set; }
        public DateTime? Time { get; set; }
        public bool? IsError { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
