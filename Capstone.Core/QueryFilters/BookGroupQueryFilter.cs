using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookGroupQueryFilter
    {
        public string Name { get; set; }
        public int? PatronId { get; set; }
        public float? Fee { get; set; }
        public float? PunishFee { get; set; }
        public int? CategoryId { get; set; }
        public string Author { get; set; }
        public bool IsNewest { get; set; }
        public bool IsPopular { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
