﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? BookGroupId { get; set; }
    }
}
