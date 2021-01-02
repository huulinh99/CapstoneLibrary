using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BorrowDetail : BaseEntity
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? BorrowId { get; set; }

        public virtual Book Book { get; set; }
        public virtual BorrowBook Borrow { get; set; }
    }
}
