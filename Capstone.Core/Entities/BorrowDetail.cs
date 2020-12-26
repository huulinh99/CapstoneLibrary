using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BorrowDetail : BaseEntity
    {
        public BorrowDetail()
        {
            BorrowBook = new HashSet<BorrowBook>();
        }

        public int? BookId { get; set; }
        public int? BorrowId { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
    }
}
