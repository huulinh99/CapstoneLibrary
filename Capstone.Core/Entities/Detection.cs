using System;
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
        public string Thumbnail { get; set; }
        public int BookShelfId { get; set; }
        public DateTime Time { get; set; }

        public virtual BookShelf BookShelf { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<DrawerDetection> DrawerDetection { get; set; }
    }
}
