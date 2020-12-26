using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Device
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
