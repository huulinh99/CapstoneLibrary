using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Staff : BaseEntity
    {
        public Staff()
        {
            BookDetect = new HashSet<BookDetect>();
            BorrowBook = new HashSet<BorrowBook>();
            Campaign = new HashSet<Campaign>();
            Device = new HashSet<Device>();
            Notification = new HashSet<Notification>();
            ReturnBook = new HashSet<ReturnBook>();
        }

        public string Name { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? DoB { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<BookDetect> BookDetect { get; set; }
        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<Device> Device { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
    }
}
