using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookRecommendQueryFilter
    {
        public int? CampaignId { get; set; }
        public int? BookId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
