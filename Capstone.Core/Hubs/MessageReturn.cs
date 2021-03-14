using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Hubs
{
    public class MessageReturn
    {
        public int StaffId { get; set; }
        public int CustomerId { get; set; }
        public List<int> ReturnBook { get; set; }
    }
}
