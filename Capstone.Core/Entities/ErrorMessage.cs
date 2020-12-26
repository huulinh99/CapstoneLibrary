using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class ErrorMessage : BaseEntity
    {
<<<<<<< HEAD
        //public int Id { get; set; }
=======
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912
        public int? BookDetectErrorId { get; set; }
        public int? DrawerId { get; set; }
        public string Description { get; set; }

        public virtual BookDetect BookDetectError { get; set; }
        public virtual Drawer Drawer { get; set; }
    }
}
