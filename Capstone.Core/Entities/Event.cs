using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Event : BaseEntity
    {
        public string Name { get; set; }
        public int? LocationId { get; set; }
        public int? StaffId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
