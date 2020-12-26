using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public int? Rating { get; set; }
        public int? BookGroupId { get; set; }
<<<<<<< HEAD
        public int? CustomerId { get; set; }
=======
        public int CustomerId { get; set; }
>>>>>>> 1610b215def855706b9b9b6c46a9cad9b1253912

        public virtual BookGroup BookGroup { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
