using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookGroupQueryFilter
    {
        public string Name { get; set; }
        public float? Fee { get; set; }
        public float? PunishFee { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
