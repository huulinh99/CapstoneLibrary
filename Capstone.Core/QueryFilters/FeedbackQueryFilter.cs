using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class FeedbackQueryFilter
    {
        public string ReviewContent { get; set; }
        public int? Rating { get; set; }
        public int? BookGroupId { get; set; }
        public int? PatronId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
