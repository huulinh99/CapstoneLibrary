using Capstone.Core.Entities;
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
        public string DrawerBarcode { get; set; }
        public string DrawerName { get; set; }
        public int BookCount { get; set; }
        public int Count { get; set; }
        public string BookShelfName { get; set; }       
        public virtual ICollection<DetectionError> DetectionError { get; set; }
        public virtual ICollection<UndefinedError> UndefinedError { get; set; }
    }
}
