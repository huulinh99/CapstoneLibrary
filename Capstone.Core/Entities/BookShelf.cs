using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookShelf
    {
        public BookShelf()
        {
            Drawer = new HashSet<Drawer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Drawer> Drawer { get; set; }
    }
}
