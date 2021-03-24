using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DrawerDto
    {
        public int Id { get; set; }
        // BookSheflId -> BookShelfId
        public int? BookShelfId { get; set; }
        public int? Row { get; set; }
        public int? BookId { get; set; }
        public string BookShelfName { get; set; }
        public int? BookGroupId { get; set; }
        public int? Col { get; set; }
        public string Barcode { get; set; }
    }
}
