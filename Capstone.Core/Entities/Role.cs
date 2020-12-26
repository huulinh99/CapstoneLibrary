using System;
using System.Collections.Generic;

namespace Capstone.Core.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Staff = new HashSet<Staff>();
        }

        //public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
