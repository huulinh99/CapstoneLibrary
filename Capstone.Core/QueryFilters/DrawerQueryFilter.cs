using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DrawerQueryFilter
    {
        // BookSheflId -> BookShelfId
        public int BookSheflId { get; set; }      
        public int RowStart { get; set; }
        public int RowEnd { get; set; }
        public int ColStart { get; set; }
        public int ColEnd { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
