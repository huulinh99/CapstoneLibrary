﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.CustomEntities
{
    public class PaginationOptions
    {
        public int DefaultPageSize { get; set; }
        public int DefaultPageNumber { get; set; }
        public int MaxPageSize { get; set; }
    }
}
