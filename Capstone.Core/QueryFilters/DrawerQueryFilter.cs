using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DrawerQueryFilter
    {
        // BookSheflId -> BookShelfId
        public int? BookSheflId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
