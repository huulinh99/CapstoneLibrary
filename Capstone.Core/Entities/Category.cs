using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            BookCategory = new HashSet<BookCategory>();
            FavouriteCategory = new HashSet<FavouriteCategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<BookCategory> BookCategory { get; set; }
        public virtual ICollection<FavouriteCategory> FavouriteCategory { get; set; }
    }
}
