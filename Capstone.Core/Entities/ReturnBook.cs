using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class ReturnBook : BaseEntity
    {
        public ReturnBook()
        {
            ReturnDetail = new HashSet<ReturnDetail>();
        }

        public int? CustomerId { get; set; }
        public DateTime? ReturnTime { get; set; }
        public int? BorrowId { get; set; }
        public int? StaffId { get; set; }

        public virtual BorrowBook Borrow { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
