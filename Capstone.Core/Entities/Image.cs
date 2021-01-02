using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Image : BaseEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? BookGroupId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
    }
}
