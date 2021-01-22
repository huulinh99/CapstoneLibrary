using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ClientMessage
    {
        public int UserId { get; set; }
        public int StaffId { get; set; }
        public string Message { get; set; }
    }
}
