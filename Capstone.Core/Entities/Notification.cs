using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Notification : BaseEntity
    {
        public int? Message { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
