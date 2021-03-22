using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class ReturnDetailQueryFilter
    {
        public int? BookId { get; set; }
        public bool? IsLate { get; set; }
        public double? PunishFee { get; set; }
        public int? CustomerId { get; set; }
        public double? Fee { get; set; }
        public bool? ByMonth { get; set; }
        public int? ReturnId { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
