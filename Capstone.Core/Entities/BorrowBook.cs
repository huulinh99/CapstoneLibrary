using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BorrowBook : BaseEntity
    {
        public BorrowBook()
        {
            BorrowDetail = new HashSet<BorrowDetail>();
            ReturnBook = new HashSet<ReturnBook>();
        }

        public int? CustomerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StaffId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
    }
}
