using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BorrowBookDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StaffId { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
    }
}
