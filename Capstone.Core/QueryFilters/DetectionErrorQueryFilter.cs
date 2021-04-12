using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.QueryFilters
{
    public class DetectionErrorQueryFilter
    {
        public int? DrawerDetectionId { get; set; }
        public string ErrorMessage { get; set; }
        public int? BookId { get; set; }
        public bool? IsConfirm { get; set; }
        public bool? IsRejected { get; set; }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
