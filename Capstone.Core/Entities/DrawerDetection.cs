using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class DrawerDetection : BaseEntity
    {
        public DrawerDetection()
        {
            DetectionError = new HashSet<DetectionError>();
        }

        public int DetectionId { get; set; }
        public int DrawerId { get; set; }
        public DateTime? Time { get; set; }

        public virtual Detection Detection { get; set; }
        public virtual Drawer Drawer { get; set; }
        public virtual ICollection<DetectionError> DetectionError { get; set; }
    }
}
