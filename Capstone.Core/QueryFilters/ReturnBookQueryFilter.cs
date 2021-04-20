using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class ReturnBookQueryFilter
    {
        public int? PatronId { get; set; }
        public DateTime? ReturnTime { get; set; }
        public bool? ByMonth { get; set; }
        public int? BorrowId { get; set; }
        public string PatronName { get; set; }
        public int? StaffId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
