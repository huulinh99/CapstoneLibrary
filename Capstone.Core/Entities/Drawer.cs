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

        public int? BookShelfId { get; set; }
        public int? ShelfRow { get; set; }
        public int? ShelfColumn { get; set; }

        public virtual BookShelf BookShelf { get; set; }
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<ErrorMessage> ErrorMessage { get; set; }
    }
}
