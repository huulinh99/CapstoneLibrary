using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Feedback : BaseEntity
    {
        public string ReviewContent { get; set; }
        public int Rate { get; set; }
        public int BookGroupId { get; set; }
        public int PatronId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual Patron Patron { get; set; }
    }
}
