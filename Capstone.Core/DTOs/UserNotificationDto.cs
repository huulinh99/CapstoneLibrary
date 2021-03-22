using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class UserNotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string BookGroupName { get; set; }
    }
}
