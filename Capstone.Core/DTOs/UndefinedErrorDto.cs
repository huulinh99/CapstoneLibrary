using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class UndefinedErrorDto
    {
        public int Id { get; set; }
        public int? DrawerDetectionId { get; set; }
        public bool? IsConfirm { get; set; }
        public bool? IsRejected { get; set; }
        public string ErrorMessage { get; set; }
        public int? TypeError { get; set; }
    }
}
