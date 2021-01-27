using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class FavouriteCategoryDto
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int?[] CategoryId { get; set; }
        public int? Rating { get; set; }
    }
}
