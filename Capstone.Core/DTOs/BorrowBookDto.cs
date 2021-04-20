using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BorrowBookDto
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public string PatronName { get; set; }
        public string Image { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
        public int? StaffId { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
    }
}
