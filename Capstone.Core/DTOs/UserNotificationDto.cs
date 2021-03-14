using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class UserNotificationDto
    {
        public string Message { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
