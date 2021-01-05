using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class TempDto
    {
        public int BookCategoryId { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
        public TempDto(int _BookCategoryId, ICollection<CategoryDto> _Categories)
        {
            BookCategoryId = _BookCategoryId;
            Categories = _Categories;
        }       
    }

}
