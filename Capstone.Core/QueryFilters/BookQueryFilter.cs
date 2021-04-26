using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookQueryFilter
    {
        public int? BookGroupId { get; set; }
        public string BookName { get; set; }
        public int? DrawerId { get; set; }
        //public int?[] BookId { get; set; }
        public string[] Barcode { get; set; }

        public int PageSize { get; set; }
        public bool? IsInDrawer { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsDeleted { get; set; }
        public int PageNumber { get; set; }
    }
}
