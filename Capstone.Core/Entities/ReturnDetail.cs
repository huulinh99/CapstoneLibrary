using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class ReturnDetail : BaseEntity
    {
        public int BookId { get; set; }
        public bool IsLate { get; set; }
        public double PunishFee { get; set; }
        public double Fee { get; set; }
        public int ReturnId { get; set; }

        public virtual Book Book { get; set; }
        public virtual ReturnBook Return { get; set; }
    }
}
