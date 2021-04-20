using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class FavouriteCategory : BaseEntity
    {
        public int PatronId { get; set; }
        public int CategoryId { get; set; }
        public int Rating { get; set; }

        public virtual Category Category { get; set; }
        public virtual Patron Patron { get; set; }
    }
}
