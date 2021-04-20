using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BorrowBookQueryFilter
    {
        public int? PatronId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ReturnToday { get; set; }
        public int? StaffId { get; set; }
        public bool? IsNewest { get; set; }
        public string PatronName { get; set; }        
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
