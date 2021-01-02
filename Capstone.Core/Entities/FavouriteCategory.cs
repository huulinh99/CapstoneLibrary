using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class FavouriteCategory : BaseEntity
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? CategoryId { get; set; }
        public int? Rating { get; set; }

        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
