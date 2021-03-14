using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            BorrowDetail = new HashSet<BorrowDetail>();
            DetectionError = new HashSet<DetectionError>();
            ReturnDetail = new HashSet<ReturnDetail>();
        }

        public int? BookGroupId { get; set; }
        public string BarCode { get; set; }
        public bool? IsAvailable { get; set; }
        public int? DrawerId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual Drawer Drawer { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
        public virtual ICollection<DetectionError> DetectionError { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
