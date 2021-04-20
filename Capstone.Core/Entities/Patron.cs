using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Patron : BaseEntity
    {
        public Patron()
        {
            BorrowBook = new HashSet<BorrowBook>();
            FavouriteCategory = new HashSet<FavouriteCategory>();
            Feedback = new HashSet<Feedback>();
            ReturnBook = new HashSet<ReturnBook>();
            UserNotification = new HashSet<UserNotification>();
        }

        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? DoB { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceToken { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
        public virtual ICollection<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
        public virtual ICollection<UserNotification> UserNotification { get; set; }
    }
}
