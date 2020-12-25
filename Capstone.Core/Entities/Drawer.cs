using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Drawer
    {
        public Drawer()
        {
            Book = new HashSet<Book>();
            ErrorMessage = new HashSet<ErrorMessage>();
        }

        public int Id { get; set; }
        public int? BookSheflId { get; set; }
        public int? ShelfRow { get; set; }
        public int? ShelfColumn { get; set; }

        public virtual BookShelf BookShefl { get; set; }
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<ErrorMessage> ErrorMessage { get; set; }
    }
}
