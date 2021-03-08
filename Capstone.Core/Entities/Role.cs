using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Customer = new HashSet<Customer>();
            Staff = new HashSet<Staff>();
        }

        public string Name { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
