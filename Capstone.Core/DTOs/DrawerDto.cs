using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class DrawerDto
    {
        // BookSheflId -> BookShelfId
        public int BookSheflId { get; set; }
        public int ShelfRow { get; set; }
        public int ShelfColumn { get; set; }
    }
}
