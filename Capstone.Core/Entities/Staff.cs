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
            ReturnBook = new HashSet<ReturnBook>();
            UserNotification = new HashSet<UserNotification>();
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
        public string Image { get; set; }
        public string DeviceToken { get; set; }
        public DateTime? CreatedTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<BookDetect> BookDetect { get; set; }
        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
        public virtual ICollection<UserNotification> UserNotification { get; set; }
    }
}
