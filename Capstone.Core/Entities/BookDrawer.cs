using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookDrawer : BaseEntity
    {
        public int? BookId { get; set; }
        public int? DrawerId { get; set; }
        public DateTime? Time { get; set; }
        public int? StaffId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Drawer Drawer { get; set; }
    }
}
