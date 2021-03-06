﻿using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BorrowDetail : BaseEntity
    {
        public int? BookId { get; set; }
        public int? BorrowId { get; set; }
        public double? Fee { get; set; }

        public virtual Book Book { get; set; }
        public virtual BorrowBook Borrow { get; set; }
    }
}
