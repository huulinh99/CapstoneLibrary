using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookDrawerQueryFilter
    {
        public int? BookId { get; set; }
        public int? DrawerId { get; set; }
        public DateTime? Time { get; set; }
        public int? StaffId { get; set; }
    }
}
