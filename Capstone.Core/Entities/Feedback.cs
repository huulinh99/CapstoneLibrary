﻿using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Feedback : BaseEntity
    {
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public int? Rating { get; set; }
        public int? BookGroupId { get; set; }
        public int CustomerId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
