using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class ErrorMessage : BaseEntity
    {
        public int? BookDetectErrorId { get; set; }
        public int? DrawerId { get; set; }
        public string Description { get; set; }

        public virtual BookDetect BookDetectError { get; set; }
        public virtual Drawer Drawer { get; set; }
    }
}
