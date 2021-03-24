using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class UndefinedErrorQueryFilter
    {
        public int? DrawerDetectionId { get; set; }
        public string ErrorMessage { get; set; }
        public int? TypeError { get; set; }
        public bool? IsConfirm { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
