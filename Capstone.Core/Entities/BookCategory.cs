using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookCategory : BaseEntity
    {
        public int? BookId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
    }
}
