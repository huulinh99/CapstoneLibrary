using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookCategoryDto
    {
        public int Id { get; set; }
        public int BookGroupId { get; set; }
        public int CategoryId { get; set; }
    }
}
