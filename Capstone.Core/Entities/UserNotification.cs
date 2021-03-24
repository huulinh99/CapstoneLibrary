using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class UserNotification : BaseEntity
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int BookGroupId { get; set; }

        public virtual BookGroup BookGroup { get; set; }
        public virtual Customer User { get; set; }
        public virtual Staff UserNavigation { get; set; }
    }
}
