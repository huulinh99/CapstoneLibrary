using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class FavouriteCategoryQueryFilter
    {
        public int? PatronId { get; set; }
        public int? CategoryId { get; set; }
        public int? Rating { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
