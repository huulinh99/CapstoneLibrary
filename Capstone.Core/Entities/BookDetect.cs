using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookDetect : BaseEntity
    {
        public BookDetect()
        {
            ErrorMessage = new HashSet<ErrorMessage>();
        }

        public int? BookId { get; set; }
        public int? StaffId { get; set; }
        public DateTime? Time { get; set; }
        public bool? IsError { get; set; }

        public virtual Book Book { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ErrorMessage> ErrorMessage { get; set; }
    }
}
