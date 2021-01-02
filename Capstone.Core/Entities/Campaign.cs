using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Campaign : BaseEntity
    {
        public Campaign()
        {
            BookRecommend = new HashSet<BookRecommend>();
        }

        public int Id { get; set; }
        public int? StaffId { get; set; }
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual ICollection<BookRecommend> BookRecommend { get; set; }
    }
}
