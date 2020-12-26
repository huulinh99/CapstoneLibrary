﻿using System;
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
            ReturnBook = new HashSet<ReturnBook>();
        }

<<<<<<< HEAD
        //public int Id { get; set; }
=======
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912
        public string Name { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<BookDetect> BookDetect { get; set; }
        public virtual ICollection<BorrowBook> BorrowBook { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<ReturnBook> ReturnBook { get; set; }
    }
}
