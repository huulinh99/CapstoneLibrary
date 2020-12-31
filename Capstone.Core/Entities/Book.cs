using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            BookDetect = new HashSet<BookDetect>();
            BookDrawer = new HashSet<BookDrawer>();
            BookRecommend = new HashSet<BookRecommend>();
            BorrowDetail = new HashSet<BorrowDetail>();
            ReturnDetail = new HashSet<ReturnDetail>();
        }

        public int? BookGroupId { get; set; }
        public string BarCode { get; set; }
        public int? BookDrawerId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual ICollection<BookDetect> BookDetect { get; set; }
        public virtual ICollection<BookDrawer> BookDrawer { get; set; }
        public virtual ICollection<BookRecommend> BookRecommend { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetail { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetail { get; set; }
    }
}
