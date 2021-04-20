using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public int? Rate { get; set; }
        public int? BookGroupId { get; set; }
        public string PatronName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Image { get; set; }
        public int PatronId { get; set; }
    }
}
