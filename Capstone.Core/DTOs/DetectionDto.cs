using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DetectionDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string BookShelfName { get; set; }
        public string Url { get; set; }
        public string ImageThumbnail { get; set; }
        public string DrawerName { get; set; }
        public DateTime? Time { get; set; }
        public string StaffName { get; set; }
        public virtual ICollection<DrawerDetection> DrawerDetection { get; set; }
    }
}
