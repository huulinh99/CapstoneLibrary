using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookGroupDto
    {
        public string Name { get; set; }
        public float Fee { get; set; }
        public float PunishFee { get; set; }
        public int Quantity { get; set; }
        public string Author { get; set; }
        public string PublishingPalace { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Thick { get; set; }
    }
}
