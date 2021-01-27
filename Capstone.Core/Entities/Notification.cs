using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Notification : BaseEntity
    {
        public string Message { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer User { get; set; }
        public virtual Staff UserNavigation { get; set; }
    }
}
