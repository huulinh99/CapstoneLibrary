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

<<<<<<< HEAD
        //public int? Id { get; set; }
=======
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912
        public string Name { get; set; }

        public virtual ICollection<BookShelf> BookShelf { get; set; }
    }
}
