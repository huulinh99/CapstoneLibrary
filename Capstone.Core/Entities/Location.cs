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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<BookShelf> BookShelf { get; set; }
    }
}
