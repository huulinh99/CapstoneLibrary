using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookDto
    {
        public int? BookGroupId { get; set; }
        public int DrawerId { get; set; }
        public string BarCode { get; set; }
    }
}
