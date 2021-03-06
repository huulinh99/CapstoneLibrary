﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public int? BookGroupId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string BarCode { get; set; }
        public string BookName { get; set; }
        public bool? IsAvailable { get; set; }
        public int? DrawerId { get; set; }
        public virtual DrawerDto Drawer { get; set; }
    }
}
