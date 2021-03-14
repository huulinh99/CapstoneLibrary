using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DetectionDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string Url { get; set; }
        public string ImageThumbnail { get; set; }
    }
}
