﻿using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Detection : BaseEntity
    {
        public Detection()
        {
            DrawerDetection = new HashSet<DrawerDetection>();
        }

        public int StaffId { get; set; }
        public string Url { get; set; }
        public string ImageThumbnail { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual ICollection<DrawerDetection> DrawerDetection { get; set; }
    }
}
