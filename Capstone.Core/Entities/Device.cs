using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Device : BaseEntity
    {
        public string DeviceToken { get; set; }
        public int CustomerId { get; set; }
        public int? StaffId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
