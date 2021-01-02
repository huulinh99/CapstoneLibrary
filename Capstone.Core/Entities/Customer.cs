using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            BorrowBook = new HashSet<BorrowBook>();
            Device = new HashSet<Device>();
            FavouriteCategory = new HashSet<FavouriteCategory>();
            Feedback = new HashSet<Feedback>();
            Notification = new HashSet<Notification>();
            ReturnBook = new HashSet<ReturnBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
    }
}
