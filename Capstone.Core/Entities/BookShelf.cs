using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookShelf : BaseEntity
    {
        public BookShelf()
        {
            Drawer = new HashSet<Drawer>();
        }
        public string Name { get; set; }
        public int? LocationId { get; set; }
        public int? Col { get; set; }
        public int? Row { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Drawer> Drawer { get; set; }
    }
}
