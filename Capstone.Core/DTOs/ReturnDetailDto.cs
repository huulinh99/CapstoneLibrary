using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ReturnDetailDto
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public bool? IsLate { get; set; }
        public double? PunishFee { get; set; }
        public string BookName { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public DateTime? ReturnTime { get; set; }
        public int? BookGroupId { get; set; }
        public int? PatronId { get; set; }
        public double? Fee { get; set; }
        public int Count { get; set; }
        public int ReturnId { get; set; }     
    }
}
