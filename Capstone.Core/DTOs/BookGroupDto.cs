﻿using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookGroupDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Fee { get; set; }
        public double? PunishFee { get; set; }
        public int? Quantity { get; set; }
        public string Author { get; set; }
        public string PublishingPlace { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public int? PageNumber { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Thick { get; set; }
        public int? PublishNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public string[] cate { get; set; }
        public virtual ICollection<CategoryDto> Category { get; set; }
        public virtual ICollection<BookCategoryDto> BookCategory { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<RatingDto> Rating { get; set; }
        public double? RatingAverage { get; set; }
    }
}
