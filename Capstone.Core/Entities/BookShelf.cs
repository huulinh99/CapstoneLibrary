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

<<<<<<< HEAD
        //public int Id { get; set; }
=======
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912
        public string Name { get; set; }
        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Drawer> Drawer { get; set; }
    }
}
