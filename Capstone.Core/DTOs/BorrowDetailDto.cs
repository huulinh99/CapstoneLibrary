﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BorrowDetailDto
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? BorrowId { get; set; }
    }
}
