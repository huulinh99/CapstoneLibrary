﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class ErrorMessageQueryFilter
    {
        public int? BookDetectErrorId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
