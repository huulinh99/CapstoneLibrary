using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookCategory : BaseEntity
    {
        public int BookGroupId { get; set; }
        public int CategoryId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual Category Category { get; set; }
    }
}
