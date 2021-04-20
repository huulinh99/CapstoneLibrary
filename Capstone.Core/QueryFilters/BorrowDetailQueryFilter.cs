using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BorrowDetailQueryFilter
    {
        public int? BorrowId { get; set; }
        public int? BookId { get; set; }
        public int? PatronId { get; set; }
        public string Barcode { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
