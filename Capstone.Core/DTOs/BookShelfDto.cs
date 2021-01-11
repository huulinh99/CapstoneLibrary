using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class BookShelfDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public string LocationColor { get; set; }
        public int? Col { get; set; }
        public int? Row { get; set; }
        public int? LocationId { get; set; }
    }
}
