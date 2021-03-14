using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DrawerDetectionDto
    {
        public int Id { get; set; }
        public int DetectionId { get; set; }
        public int DrawerId { get; set; }
        public DateTime? Time { get; set; }
    }
}
