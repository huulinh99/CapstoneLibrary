using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Drawer : BaseEntity
    {
        public Drawer()
        {
            Book = new HashSet<Book>();
            ErrorMessage = new HashSet<ErrorMessage>();
        }

<<<<<<< HEAD
        //public int Id { get; set; }
=======
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912
        public int? BookSheflId { get; set; }
        public int? ShelfRow { get; set; }
        public int? ShelfColumn { get; set; }

        public virtual BookShelf BookShefl { get; set; }
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<ErrorMessage> ErrorMessage { get; set; }
    }
}
