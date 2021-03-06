﻿using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class BookGroup : BaseEntity
    {
        public BookGroup()
        {
            Book = new HashSet<Book>();
            BookCategory = new HashSet<BookCategory>();
            Feedback = new HashSet<Feedback>();
            Image = new HashSet<Image>();
        }

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

        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<BookCategory> BookCategory { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Image> Image { get; set; }
    }
}
