using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ReturnBookDto
    {
        public int? CustomerId { get; set; }
        public DateTime? ReturnTime { get; set; }
        public int? BorrowId { get; set; }
        public int? StaffId { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
