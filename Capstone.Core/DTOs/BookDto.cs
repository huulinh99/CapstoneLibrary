using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public int BookGroupId { get; set; }
        public int? PatronId { get; set; }
        public string PatronName { get; set; }
        public string DrawerName { get; set; }
        public string PatronImage { get; set; }
        public string BarCode { get; set; }
        public string DrawerBarCode { get; set; }
        public string BookShelfName { get; set; }
        public string LocationName { get; set; }
        public string BookName { get; set; }
        public bool IsAvailable { get; set; }
        public int? DrawerId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual DrawerDto Drawer { get; set; }
    }
}
