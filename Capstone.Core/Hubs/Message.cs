using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Hubs
{
    public class Message
    {
        public int StaffId { get; set; }
        public int PatronId { get; set; }
        public List<int> Wishlist { get; set; }       
    }
}
