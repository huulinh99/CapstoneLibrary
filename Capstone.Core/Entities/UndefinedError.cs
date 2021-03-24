using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class UndefinedError : BaseEntity
    {
        public int DrawerDetectionId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRejected { get; set; }
        public int TypeError { get; set; }
        public bool IsConfirm { get; set; }

        public virtual DrawerDetection DrawerDetection { get; set; }
    }
}
