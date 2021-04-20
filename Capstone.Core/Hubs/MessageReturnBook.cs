using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Hubs
{
    public class MessageReturnBook
    {
        public int StaffId { get; set; }
        public int PatronId { get; set; }
        public string Barcode { get; set; }
    }
}
