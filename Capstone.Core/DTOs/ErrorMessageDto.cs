using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ErrorMessageDto
    {
        public int Id { get; set; }
        public int BookDetectErrorId { get; set; }
        public int DrawerId { get; set; }
        public int Description { get; set; }
    }
}
