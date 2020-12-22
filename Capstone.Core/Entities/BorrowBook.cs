using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BorrowBook
    {
        public BorrowBook()
        {
            ReturnBook = new HashSet<ReturnBook>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StaffId { get; set; }

        public virtual BorrowDetail Customer { get; set; }
        public virtual Customer CustomerNavigation { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
    }
}
