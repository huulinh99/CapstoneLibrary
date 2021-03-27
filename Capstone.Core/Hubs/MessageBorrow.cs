using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Hubs
{
    public class MessageBorrow
    {
        public int StaffId { get; set; }
        public int CustomerId { get; set; }
        public string Barcode { get; set; }
    }
}
