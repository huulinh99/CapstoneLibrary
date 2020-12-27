using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookCategoryQueryFilter
    {
        public int? BookId { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
