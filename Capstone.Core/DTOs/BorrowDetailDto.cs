using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BorrowDetailDto
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? CustomerId { get; set; }
        public string BookName { get; set; }
        public string Image { get; set; }
        public string Barcode { get; set; }
        public string Author { get; set; }
        public bool IsReturn { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public int? BorrowId { get; set; }
        public int? BookGroupId { get; set; }
        public int Count { get; set; }
        public double? Fee { get; set; }
        public double? PunishFee { get; set; }
    }
}
