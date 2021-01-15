using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Location : BaseEntity
    {
        public Location()
        {
            BookShelf = new HashSet<BookShelf>();
        }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsRoom { get; set; }

        public virtual ICollection<BookShelf> BookShelf { get; set; }
    }
}
