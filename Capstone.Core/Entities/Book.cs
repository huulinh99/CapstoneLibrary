using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            BookCategory = new HashSet<BookCategory>();
            BookDetect = new HashSet<BookDetect>();
            BookRecommend = new HashSet<BookRecommend>();
            BorrowDetail = new HashSet<BorrowDetail>();
            Drawer = new HashSet<Drawer>();
            ReturnDetail = new HashSet<ReturnDetail>();
        }

        public int? BookGroupId { get; set; }
        public int? DrawerId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual ICollection<BookCategory> BookCategory { get; set; }
        public virtual ICollection<BookDetect> BookDetect { get; set; }
        public virtual ICollection<BookRecommend> BookRecommend { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
        public virtual ICollection<Drawer> Drawer { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
