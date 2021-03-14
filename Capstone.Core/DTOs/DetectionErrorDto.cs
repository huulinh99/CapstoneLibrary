using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DetectionErrorDto
    {
        public int Id { get; set; }
        public int DrawerDetectionId { get; set; }
        public string ErrorMessage { get; set; }
        public int BookId { get; set; }
        public bool? IsError { get; set; }
    }
}
