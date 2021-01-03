using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookRecommend : BaseEntity
    {
        public int? CampaignId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}
