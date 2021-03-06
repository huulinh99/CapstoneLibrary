﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class BookShelfQueryFilter
    {
        public int? LocationId { get; set; }
        public int? DrawerId { get; set; }
        public int? BookGroupId { get; set; }

        public string Name { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
