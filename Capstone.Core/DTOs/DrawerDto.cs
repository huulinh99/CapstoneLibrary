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
        public int? ShelfRow { get; set; }
        public int? ShelfColumn { get; set; }
    }
}
