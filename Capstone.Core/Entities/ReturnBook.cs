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

        public int PatronId { get; set; }
        public DateTime ReturnTime { get; set; }
        public int BorrowId { get; set; }
        public int StaffId { get; set; }
        public double Fee { get; set; }

        public virtual BorrowBook Borrow { get; set; }
        public virtual Patron Patron { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
