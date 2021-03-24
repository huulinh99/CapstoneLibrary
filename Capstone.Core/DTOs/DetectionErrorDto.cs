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
        public string BookName { get; set; }
        public bool? IsConfirm { get; set; }
        public string BookBarcode { get; set; }
        public int BookId { get; set; }
        public int? TypeError { get; set; }
        public bool? IsRejected { get; set; }
    }
}
