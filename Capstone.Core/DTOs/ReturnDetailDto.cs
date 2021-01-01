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
        public string Fee { get; set; }
        public int ReturnId { get; set; }     
    }
}
