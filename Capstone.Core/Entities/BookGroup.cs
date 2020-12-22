using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookGroup
    {
        public BookGroup()
        {
            Book = new HashSet<Book>();
            Feedback = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Fee { get; set; }
        public double? PunishFee { get; set; }

        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
